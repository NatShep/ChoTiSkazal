﻿using System;
using System.Linq;
using Chotiskazal.Logic.Services;
using Dic.Logic.DAL;

namespace Chotiskazal.App.Modes
{
    class GraphsStatsMode: IConsoleMode
    {
        public string Name => "Show stats";
        public void Enter(NewWordsService service)
        {
            var allWords = service.GetAll();

            RenderKnowledgeHistogram(allWords);
            Console.WriteLine();
            Console.WriteLine();
            RenderAddingTimeLine(allWords);
            RenderExamsTimeLine(service.GetAllExams());

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();    
            
            Console.WriteLine($"Context phrases count = {service.GetContextPhraseCount()}");
            Console.WriteLine($"Words count = {allWords.Count(w=>!w.OriginWord.Contains(' '))}");
            Console.WriteLine($"Words and sentences count = {allWords.Length}");


            var groups = allWords
                .GroupBy(s => s.State)
                .OrderBy(s => (int)s.Key)
                .Select(s => new { state = s.Key, count = s.Count() });

            var doneCount = allWords.Count(a => a.PassedScore >= PairModel.MaxExamScore);

            Console.WriteLine($"Done: {doneCount} words  ({(doneCount * 100 / allWords.Length)}%)");
            Console.WriteLine($"Unknown: {allWords.Length - doneCount} words");
            Console.WriteLine();
            var learningRate = GetLearningRate(allWords);

            Console.WriteLine("Score is "+ learningRate);
            if (learningRate<100)
                Console.WriteLine("You has to add more words!");
            else if (learningRate < 200)
                Console.WriteLine("It's time to add new words!");
            else if (learningRate <300)
                Console.WriteLine("Zen!");
            else if (learningRate < 400)
                Console.WriteLine("Let's do some exams");
            else
            {
                Console.WriteLine("Exams exams exams!");
                Console.WriteLine($"You have to make at least {(learningRate-300)/10} more exams");
            }


        }

        private static void RenderKnowledgeHistogram(PairModel[] allWords)
        {
            var length = 19;
            var wordHystogramm = new int[length];

            int maxCount = 0;
            foreach (var pairModel in allWords)
            {
                var score = pairModel.PassedScore;
                if (score >= wordHystogramm.Length)
                    score = wordHystogramm.Length - 1;
                wordHystogramm[score]++;
                maxCount = Math.Max(wordHystogramm[score], maxCount);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("     Knowledge histogram (v: words amount, h: knowledge)\r\n");
            Console.Write("  ");
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int row = 0; row < wordHystogramm.Length; row++)
            {
                Console.Write("____");
            }

            Console.Write("\r\n");

            int height = 15;
            for (int line = 0; line < height; line++)
            {
                Console.Write(" |");

                for (int row = 0; row < wordHystogramm.Length; row++)
                {
                    var rowHeight = Math.Ceiling(((height * wordHystogramm[row]) / (double) maxCount));
                    if (rowHeight >= height - line)
                        Console.Write("|_| ");
                    else
                        Console.Write("    ");
                }

                Console.Write("|\r\n");
            }

            Console.Write(" |");
            for (int row = 0; row < wordHystogramm.Length; row++)
            {
                Console.Write("____");
            }

            Console.Write("|\r\n");
            Console.ResetColor();

        }

        private static int GetLearningRate(PairModel[] allModels)
        {

            //PairModel.MaxExamScore+2, a.AggedScore 
            double sum= 0;
            foreach (var pair in allModels)
            {
                var hiLim = PairModel.MaxExamScore + 2;
                if (pair.PassedScore < hiLim)
                    sum += pair.PassedScore;
                else
                {
                    sum+= Math.Min(hiLim, Math.Max(pair.AggedScore, hiLim - 1));
                }
            }

            var count = allModels.Count();
            return (int) (count * PairModel.MaxExamScore -sum);
        }

        private static void RenderAddingTimeLine(PairModel[] allWords)
        {
            var wordTimeline = new int[21];

            int maxCount = 0;
            foreach (var pairModel in allWords)
            {
                var score = (int)(DateTime.Now.Date - pairModel.Created.Date).TotalDays +1;
                if(score>wordTimeline.Length || score<0)
                    continue;
                wordTimeline[^score]++;
                maxCount = Math.Max(wordTimeline[^score], maxCount);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     Add History (v: words amount, h: days)\r\n");
            Console.Write("  ");

            for (int row = 0; row < wordTimeline.Length; row++)
            {
                Console.Write("____");
            }

            Console.Write("\r\n");

            int height = 15;
            for (int line = 0; line < height; line++)
            {
                Console.Write(" |");

                for (int row = 0; row < wordTimeline.Length; row++)
                {
                    var rowHeight = Math.Round(((height * wordTimeline[row]) / (double)maxCount));
                    if (rowHeight >= height - line)
                        Console.Write("|_| ");
                    else
                        Console.Write("    ");
                }

                Console.Write("|\r\n");
            }

            Console.Write(" |");
            for (int row = 0; row < wordTimeline.Length; row++)
            {
                Console.Write("____");
            }

            Console.Write("|\r\n");
            Console.ResetColor();

        }
        private static void RenderExamsTimeLine(Exam[] exams)
        {
            var wordTimeline = new int[21];
            int maxCount = 0;
            Console.ForegroundColor = ConsoleColor.DarkRed;

            foreach (var pairModel in exams)
            {
                var score = (int)(DateTime.Now.Date - pairModel.Started.Date).TotalDays + 1;
                if (score > wordTimeline.Length || score < 0)
                    continue;
                wordTimeline[^score]++;
                maxCount = Math.Max(wordTimeline[^score], maxCount);
            }
           
            int height = 15;
            for (int line = 0; line < height; line++)
            {
                Console.Write(" |");

                for (int row = 0; row < wordTimeline.Length; row++)
                {
                    var rowHeight = Math.Ceiling(((height * wordTimeline[row]) / (double)maxCount));
                    if (rowHeight >= height - line)
                        Console.Write("|_| ");
                    else
                        Console.Write("    ");
                }

                Console.Write("|\r\n");
            }

            Console.Write(" |");
            for (int row = 0; row < wordTimeline.Length; row++)
            {
                Console.Write("____");
            }

            Console.Write("|\r\n");
            Console.Write("     Exams History (v: exams amount, h: days)\r\n");
            Console.Write("  ");

            Console.ResetColor();

        }
    }
}
