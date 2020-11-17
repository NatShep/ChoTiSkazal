﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chotiskazal.Bot.Questions;
using Chotiskazal.Bot.Services;
using Chotiskazal.Dal.DAL;
using Chotiskazal.DAL.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace Chotiskazal.Bot.ChatFlows
{
    public class ExamFlow
    {
        private readonly ChatIO _chatIo;
        private readonly ExamService _examService;

        public ExamFlow(ChatIO chatIo, ExamService service)
        {
            _chatIo = chatIo;
            _examService = service;
        }

        public async Task EnterAsync(int userId)
        {
            if (!await _examService.HasAnyAsync(userId))
            {
                await _chatIo.SendMessageAsync("You need to add some words before examination");
                return;
            }

            //Randomization and jobs
            await _examService.RandomizationAndJobsAsync(userId);

            var sb = new StringBuilder("Examination\r\n");
            var learningWords = await _examService.GetWordsForLearningWithPhrasesAsync(userId, 9, 3);
            if (learningWords.Average(w => w.PassedScore) <= 4)
            {
                foreach (var pairModel in learningWords.Randomize())
                {
                    sb.AppendLine($"{pairModel.EnWord}\t\t:{pairModel.UserTranslations}");
                }
            }

            var startMessageSending = _chatIo.SendMessageAsync(sb.ToString(), new InlineKeyboardButton
            {
                CallbackData = "/startExamination",
                Text = "Start"
            }, new InlineKeyboardButton
            {
                CallbackData = "/start",
                Text = "Cancel",
            });

            //Get exam list and test words
            var examsList = _examService.PreparingExamsList(learningWords);
            var testWords = await _examService.GetTestWordsAsync(userId, examsList);
            examsList.AddRange(testWords);

            var examsCount = 0;
            var examsPassed = 0;
            var i = 0;
            ExamResult? lastExamResult = null;

            await startMessageSending;
            var started = DateTime.Now;
            var userInput = await _chatIo.WaitInlineKeyboardInput();
            if (userInput != "/startExamination")
                return;
            foreach (var pairModel in examsList)
            {
                var exam = ExamSelector.GetNextExamFor(i < 9, pairModel);
                i++;
                var retryFlag = false;
                do
                {
                    retryFlag = false;
                    var sw = Stopwatch.StartNew();
                    var questionMetric = _examService.CreateQuestionMetric(pairModel, exam);

                    var learnList = learningWords;

                    if (!learningWords.Contains(pairModel))
                        learnList = learningWords.Append(pairModel).ToArray();

                    if (exam.NeedClearScreen && lastExamResult != ExamResult.Impossible)
                    {
                        await WriteDontPeakMessage();
                        if (lastExamResult == ExamResult.Passed)
                            await WritePassed();
                    }

                    var result = await exam.Pass(_chatIo, _examService, pairModel, learnList);

                    sw.Stop();
                    questionMetric.ElaspedMs = (int) sw.ElapsedMilliseconds;
                    switch (result)
                    {
                        case ExamResult.Impossible:
                            exam = ExamSelector.GetNextExamFor(i == 0, pairModel);
                            retryFlag = true;
                            break;
                        case ExamResult.Passed:
                            await WritePassed();
                            await _examService.SaveQuestionMetrics(questionMetric);
                            examsCount++;
                            examsPassed++;
                            break;
                        case ExamResult.Failed:
                            await WriteFailed();
                            questionMetric.Result = 0;
                            await _examService.SaveQuestionMetrics(questionMetric);
                            examsCount++;
                            break;
                        case ExamResult.Retry:
                            retryFlag = true;
                            Console.WriteLine();
                            Console.WriteLine();
                            break;
                        case ExamResult.Exit: return;
                    }

                    lastExamResult = result;

                } while (retryFlag);

                await _examService.RegisterExamAsync(userId, started, examsCount, examsPassed);
            }

            var doneMessage = new StringBuilder($"Test done:  {examsPassed}/{examsCount}\r\n");
            foreach (var pairModel in learningWords.Concat(testWords))
            {
                doneMessage.Append(pairModel.EnWord + " - " + pairModel.UserTranslations + "  (" +
                                   pairModel.PassedScore + ")\r\n");
            }

            await _chatIo.SendMessageAsync(doneMessage.ToString());
        }

        private async Task WriteDontPeakMessage()
        {
            await _chatIo.SendMessageAsync(
                "\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.\r\n.");
            await _chatIo.SendMessageAsync(
                "Don't peek");
            await _chatIo.SendMessageAsync("\U0001F648");
        }

        private Task WriteFailed() => _chatIo.SendMessageAsync("Noo... \U0001F61E");

        private Task WritePassed() => _chatIo.SendMessageAsync($"It's right! \U0001F609");
    }
}
