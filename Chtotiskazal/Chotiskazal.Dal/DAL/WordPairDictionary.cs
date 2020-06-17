﻿using Chotiskazal.LogicR.yapi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chotiskazal.DAL
{
    public class WordPairDictionary
    {

        public WordPairDictionary() { }

        public WordPairDictionary(string originword, string translation, string transcription, string sourse)
        {
            EnWord = originword;
            Transcription = transcription;
            RuWord = translation;
            Sourse = sourse;
        }
        public WordPairDictionary(string originword, string translation, string transcription, string sourse, List<PhraseDictionary> phrases)
        {
            EnWord = originword;
            Transcription = transcription;
            RuWord = translation;
            Phrases = phrases;
            Sourse = sourse;
        }
        public long Id { get; set; }
        public string EnWord { get; set; }

        //for one Word has one Translation
        //in table WordsWithTranslation we have same words with different ID and different Translate
        // or we can use composite key(EnWord+Translate).      
        public string RuWord { get; set; }
        public string Transcription { get; set; }

        /* ALTERNATIVE 
               //if IsPhrase is true WordSourse=Id of Word in this table
               //if IsPhrase is false WordSourse=null
               public bool IsPhrase { get; set; }
               public int WordSourse { get;set; }
        */

        public List<PhraseDictionary> Phrases { get; set; }
        public string Sourse { get; set; }

        public void CreateFromYandexDTO(YaDefenition yandexDTO)
        {

        }
    }
}
