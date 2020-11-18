﻿using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chotiskazal.Bot.Services;
using Chotiskazal.Dal.DAL;
using Chotiskazal.DAL.Services;

namespace Chotiskazal.Bot.Questions
{
    public class RuPhraseSubstituteExam : IExam
    {
        public bool NeedClearScreen => false;

        public string Name => "Ru phrase substitute";
        public async Task<ExamResult> Pass(ChatIO chatIo, ExamService service, UserWordForLearning word, UserWordForLearning[] examList)
        {
            if (!word.Phrases.Any())
                return ExamResult.Impossible;

            var phrase = word.Phrases.GetRandomItem();
            
            var replaced = phrase.PhraseRuTranslate.Replace(phrase.EnWord, "...");
            if (replaced == phrase.PhraseRuTranslate)
                return ExamResult.Impossible;

            var sb = new StringBuilder();
            
            sb.AppendLine($"\"{phrase.EnPhrase}\"");
            sb.AppendLine($" translated as ");
            sb.AppendLine($"\"{replaced}\"");
            sb.AppendLine();
            sb.AppendLine($"Enter missing word: ");
            
            while (true)
            {
                var enter = await chatIo.WaitUserTextInputAsync();
                if (string.IsNullOrWhiteSpace(enter))
                    continue;
                if (string.CompareOrdinal(phrase.EnWord.ToLower().Trim(), enter.ToLower().Trim()) == 0)
                {
                    await service.RegisterSuccessAsync(word);
                    return ExamResult.Passed;
                }

                await chatIo.SendMessageAsync($"Origin phrase was \"{phrase.PhraseRuTranslate}\"");
                await service.RegisterFailureAsync(word);
                return ExamResult.Failed;
            }
        }
    }
}