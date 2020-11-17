﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Chotiskazal.Bot.Services;
using Chotiskazal.Dal.DAL;

namespace Chotiskazal.Bot.Questions
{
    public class EngWriteExam : IExam
    {
        public bool NeedClearScreen => false;

        public string Name => "Eng Write";

        public async Task<ExamResult> Pass(ChatIO chatIo, ExamService service, UserWordForLearning word,
            UserWordForLearning[] examList)
        {
            var translations = word.GetTranslations().ToArray();
            
            var minCount = translations.Min(t => t.Count(c => c == ' '));
            if (minCount > 0 && word.PassedScore < minCount * 4)
                return ExamResult.Impossible;

            await chatIo.SendMessageAsync($"=====>   {word.EnWord}    <=====\r\nWrite the translation... ");
            var translation = await chatIo.WaitUserTextInputAsync();
           
            if (string.IsNullOrEmpty(translation))
                return ExamResult.Retry;

            if (translations.Any(t => string.Compare(translation, t, StringComparison.OrdinalIgnoreCase) == 0))
            {
                await service.RegisterSuccessAsync(word);
                return ExamResult.Passed;
            }

            var allMeaningsOfWord = await service.GetAllMeaningOfWordForExamination(word.EnWord);
          
            if (allMeaningsOfWord
                .Any(t => string.Compare(translation, t, StringComparison.OrdinalIgnoreCase) == 0))
            {
                await chatIo.SendMessageAsync(
                    $"Chosen translation is out of scope (but it is correct). Expected translations are: " +
                    word.UserTranslations);
                return ExamResult.Impossible;
            }

            await chatIo.SendMessageAsync("The translation was: " + word.UserTranslations);
            await service.RegisterFailureAsync(word);
            return ExamResult.Failed;
        }
    }
}