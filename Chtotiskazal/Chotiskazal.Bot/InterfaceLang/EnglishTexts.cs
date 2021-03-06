﻿using SayWhat.Bll;
using SayWhat.Bll.Dto;
// ReSharper disable InconsistentNaming

namespace Chotiskazal.Bot.InterfaceLang
{
    public class EnglishTexts : IInterfaceTexts
    {
        public string more { get; } = "more";
        public string thenClickStartMarkdown { get; }="then click start";
        public string ChooseTheTranslation { get; } = "Choose the translation";
        public string translatesAs { get; } = "translates as";
        public string ChooseMissingWord { get; } = "Choose missing word";
        public string OriginWas { get; } = "Origin was";
        public string EnterMissingWord { get; } = "Enter missing word";
        public string TypoAlmostRight { get; } = "Almost right. But you have a typo. Let's try again";
        public string InterfaceLanguageSetupped { get; } = "Interface language: English";
        public string JustOneLearnedWord { get; } = "You have learned just one word\\!";

        public string OutOfScopeWithCandidateMarkdown(string otherMeaning)
            => $"Chosen translation is out of scope (did you mean *\"{otherMeaning}\"*?)\\. Expected translations are";
        public string OutOfScopeTranslationMarkdown { get; } =
            "Chosen translation is out of scope \\(but it is correct\\)\\. Expected translations are";
        public string FailedTranslationWasMarkdown { get; } = "The translation was";
        public string ItIsNotRightTryAgain { get; } = "No. It is not right. Try again";
        public string SeeTheTranslation { get; } = "See the translation";
        public string DoYouKnowTranslation { get; } = $"Do you know the translation?";
        public string TranslationIs { get; } = "Translation is";
        public string DidYouGuess { get; } = "Did you guess?";
        public string IsItRightTranslation { get; } = "Is it right translation?";
        public string Mistaken { get; } = "Mistaken";
        public string ChooseWhichWordHasThisTranscription { get; } = "Choose which word has this transcription";
        public string RetryAlmostRightWithTypo { get; } = "Almost right. But you have a typo. Let's try again";
        public string ShowTheTranslationButton { get; } = "Show the translation";
        public string WriteTheTranslationMarkdown { get; } = $"Write the translation\\.\\.\\. ";
        public string RightTranslationWas { get; } = "The right translation was";
        public string CorrectTranslationButQuestionWasAbout { get; } =
            "Your translation was correct, but the question was about the word";
        public string LetsTryAgain { get; } = "Let's try again";
        public string ChooseTheTranscription { get; } = "Choose the transcription";
        public string WordsInPhraseAreShuffledWriteThemInOrderMarkdown { get; } =
            "Words in phrase are shuffled\\. Write them in correct order";
        public string YouHaveATypoLetsTryAgainMarkdown(string text)
            => $"You have a typo\\. Correct spelling is *\"{text}\"*\\. Let's try again\\.";
       
        #region questionResult
        public string Passed1Markdown { get; } = "Ayeee\\!";
        public string PassedOpenIHopeYouWereHonestMarkdown { get; } = "Good\\. I hope you were honest";
        public string PassedHideousWellMarkdown { get; } = "Well";
        public string PassedHideousWell2 { get; } = "Good";
        public string FailedOpenButYouWereHonestMarkdown { get; } = "But you were honest\\.\\.\\.";
        public string FailedHideousHonestyIsGoldMarkdown { get; } = "Honesty is gold";
        public string FailedMistakenMarkdown(string text)
            => $"Mistaken\\. Correct spelling is '{text}'";
        public object FailedOriginExampleWasMarkdown { get; } = "Wrong\\. Origin phrase was";

        public object FailedOriginExampleWas2 { get; } = "Origin phrase was";
        public object FailedOriginExampleWas2Markdown { get; } = "Origin phrase was";

        public string FailedDefaultMarkdown { get; }= "Noo\\.\\.\\.";
        public string PassedDefaultMarkdown { get; }= "It's right\\!";
        public string IgnoredDefaultMarkdown { get; }= "So so\\.\\.\\.";
        public string FailedHideousDefaultMarkdown { get; }= "Last answer was wrong";
        public string PassedHideousDefaultMarkdown { get; }= "Last answer was right";
        
        public string IgnoredHideousDefault { get; }= "Not really";
         #endregion

        public string DidYouWriteSomething { get; } = "Did you write something? I was asleep the whole time...";

        public string EnterWordOrStart { get; } =
            "Enter english or russian word to translate or /start to open main menu ";

        public string NoTranslationsFound { get; } = "No translations found. Check the word and try again";

        public string LearningCarefullyStudyTheListMarkdown { get; } = "*Learning*\r\n"+
                                                                    "Carefully study the words in the list below:";
        
        public object LearningDone { get; } = "Learning done";
        public object WordsInTestCount { get; } = "Words in test";
        public object YouHaveLearnedOneWord { get; } = "You have learned one word";
        public object YouForgotOneWord { get; } ="You forgot one word";
        public object EarnedScore { get; } = "Earned score";
        public object TotalScore { get; } = "Total score";
        public object DontPeekUpward { get; } = "Now try to answer without hints. Don't peek upward!";

        public string NeedToAddMoreWordsBeforeLearning { get; } =
            "You need to add some more words before examination";

        public object less { get; } = "less";
   
        public string HelpMarkdown { get; } = "*Hello\\! I am a translator and teacher\\.*\r\n\r\n" +
                                                   "1⃣ You can use me as a regular translator\\. " +
                                                   "Just write the word for translation or use /add command to begin translate\\.\r\n\r\n" +
                                                   "2⃣ Then, when you have time and mood, click on the _\"Learn\"_ button or " +
                                                   "write /learn and start learning this words\\.\r\n\r\n" +
                                                   "3⃣ Earn scores for your action and watch your progress using /stats command\\.\r\n\r\n" +
                                                   "4⃣ Use /help command to see info how it works\\.\r\n\r\n" +
                                                   "\uD83D\uDE09Yes, it's free\\. We have done this bot for us and our friends\\. " +
                                                   "And we hope it makes you a little bit happy and gonna learn billion of words\\. We ve checked it\\!";

        public string MainMenuTextMarkdown { get; } = "I am a translator and teacher\\.\r\n" +
                                                   "First you can use me as a regular translator\\." +
                                                   "After that " +
                                                   "learn this words and it helps you to speak English easily\\.\r\n\r\n" +
                                                   "*Just try it and see for yourself\\!*";

        public string ActionIsNotAllowed { get;  } = "action is not allowed";
        public string OopsSomethingGoesWrong { get;  } = "Oops. Something goes wrong ;( \r\nWrite /start to go to main menu.";

        public string HereAreTheTranslationMarkdown(string word, string? tr)
            => $"_Here are the translations\\._ \r\n" +
               $"_Choose one of them to learn them in the future_\r\n\r\n" +
               $"*{word.EscapeForMarkdown().Capitalize()}*" +
               $"{(tr == null ? "\r\n" : $"\r\n```\r\n[{tr.EscapeForMarkdown()}]\r\n```")}";

        public string MessageAfterTranslationIsSelected(DictionaryTranslation translation)

            => $"Translation  '{translation.TranslatedText} - {translation.OriginText}' is saved";
        public string MessageAfterTranslationIsDeselected(DictionaryTranslation translation)
            => $"Translation  '{translation.TranslatedText} - {translation.OriginText}' is removed";

        public string LearnMoreWordsMarkdown(in int length)
            => $"Good job\\! You have learned {length} words\\!";

        public string LearnSomeWordsMarkdown(in int length)
            =>$"You have learned {length} words\\. Let's do more\\!";

        public string ShowNumberOfLists(in int number,in int count)
            => $"\r\n`Show {number} list of {count}\\.\\.\\.`";

        public string YouHaveLearnedWords(in int count)
            => $"You have learned {count} words";

        public string YouForgotCountWords(in int forgottenWordsCount)
            =>$"You forgot {forgottenWordsCount} words";
        
        
        #region buttons
        public string YesButton { get; } = "Yes";
        public string NoButton { get; } = "No";
        public string StartButton { get; } = "Start";
        public string CancelButton { get; } = "Cancel";
        public object OneMoreLearnButton { get; } = "One more learn";
        public string TranslateButton { get; } = "Translate";
        public string ContinueTranslateButton { get; } = "Continue";
        public string LearnButton { get; } = "Learn";
        public string StatsButton { get; } = "Stats";
        public string HelpButton { get; } = "Help";
        public string MainMenuButton { get; } = "Main menu";

        public string ShowWellKnownWords { get; } = "My learned words";
        public string NoWellKnownWords { get; } = "You haven't learned words\\!";

        #endregion
        
        #region stats
        public string[] ShortDayNames { get; } = {
            "mon",
            "tue",
            "wed",
            "thu",
            "fri",
            "sat",
            "sun"
        };

        public string Zen1WeNeedMuchMoreNewWords { get; } = "We need much more new words!";
        public string Zen2TranslateNewWords { get; } = "Translate new words";
        public string Zen3TranslateNewWordsAndPassExams { get; } = "Translate new words and pass exams.";

        public string Zen3EverythingIsGood { get; } = $"Everything is perfect! " +
                                                      $"\r\nTranslate new words and pass exams.";

        public string Zen4PassExamsAndTranslateNewWords { get; } = "Pass exams and translate new words.";
        public string Zen5PassExams { get; } = "I recommend to pass exams";
        public string Zen6YouNeedToLearn { get; } = $"Learning learning learning!";
        public object StatsYourStats { get; } = "Your stats";
        public object StatsWordsAdded { get; } = "Words added";
        public object StatsLearnedWell { get; } = "Learned well";
        public object StatsScore { get; } = "Score";
        public object StatsExamsPassed { get; } = "Exams passed";
        public object StatsThisMonth { get; } = "This month";
        public object StatsThisDay { get; } = "This day";
        public object StatsActivityForLast7Weeks { get; } = "Activity during last 7 weeks";
        #endregion
    }
}