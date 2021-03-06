﻿using NUnit.Framework;
using SayWhat.Bll;

namespace SayWhat.MongoDAL.Tests
{
    public class StringHelperTest
    {
        [TestCase("origin","Origin")]
        [TestCase("ВАСЯ","Вася")]
        [TestCase("петька","Петька")]
        public void CapitalizeTest(string origin, string expected) => Assert.AreEqual(expected,origin.Capitalize());
        
        [TestCase("a","a")]
        [TestCase("","")]
        [TestCase("abc","aBc")]
        [TestCase(" abc"," aBc")]
        [TestCase("Иди ты в пень","Иди ты в пень")]
        public void CheckMistakes_returnsEqual(string wordA, string wordB) 
            => Assert.AreEqual(StringsCompareResult.Equal,wordA.CheckForMistakes(wordB));
        
        
        [TestCase("meaningfull","meaningful")]
        [TestCase("meaningfull","meaninfull")]
        [TestCase("meaningfull","meanignfull")]
        [TestCase("meaningfull","meaningful")]
        [TestCase("безболезненный","безболезненый")]
        public void CheckMistakes_returnsSmallMistakes(string wordA, string wordB) 
            => Assert.AreEqual(StringsCompareResult.SmallMistakes,wordA.CheckForMistakes(wordB));
        
        
        [TestCase("meaningfull","meninful")]
        [TestCase("meaningfull","meaninful")]
        [TestCase("безболезненный","безбоелзнненый")]
        public void CheckMistakes_returnsBigMistakes(string wordA, string wordB) 
            => Assert.AreEqual(StringsCompareResult.BigMistakes,wordA.CheckForMistakes(wordB));
        [TestCase("Chekc","che")]
        [TestCase("a","b")]
        [TestCase("вася","петя")]
        public void CheckMistakes_returnsNotEqual(string wordA, string wordB) 
            => Assert.AreEqual(StringsCompareResult.NotEqual,wordA.CheckForMistakes(wordB));
        
    }
}