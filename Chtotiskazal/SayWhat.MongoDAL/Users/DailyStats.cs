﻿using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SayWhat.MongoDAL.Users
{
    public class StatsBase
    {
        [BsonElement("wa")]
        [BsonDefaultValue(0)]
        [BsonIgnoreIfDefault]
        public int WordsAdded { get; set; }

        [BsonElement("ec")]
        [BsonDefaultValue(0)]
        [BsonIgnoreIfDefault]
        public int ExamplesAdded { get; set; }

        [BsonElement("w")]
        [BsonDefaultValue(0)]
        [BsonIgnoreIfDefault]
        public int WordsLearnt { get; set; }
        
        [BsonElement("pc")]
        [BsonDefaultValue(0)]
        [BsonIgnoreIfDefault]
        public int PairsAdded { get; set; }
        
        [BsonElement("qp")]
        [BsonDefaultValue(0)]
        [BsonIgnoreIfDefault]
        public int QuestionsPassed { get; set; }
        
        [BsonElement("qf")]
        [BsonDefaultValue(0)]
        [BsonIgnoreIfDefault]
        public int QuestionsFailed { get; set; }
        
        [BsonElement("ld")]
        [BsonIgnoreIfDefault]
        public int LearningDone { get; set; }
        
        [BsonElement("s")]
        [BsonIgnoreIfDefault]
        public int TotalScore { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class TotalStats : StatsBase
    {
        
    }
    [BsonIgnoreExtraElements]
    public class DailyStats: StatsBase
    {
        [BsonElement("d")]
        [BsonRequired]
        public ushort Day { get; set; }
        
        private static readonly DateTime DayCountStarts = new DateTime(2020, 1, 1);
        
        [BsonIgnore]
        public DateTime Date
        {
            get => DayCountStarts.AddDays(Day);
            set => Day = (ushort) (value - DayCountStarts).TotalDays;
        }

    }

    public class MonthsStats : StatsBase
    {
        [BsonElement("m")]
        [BsonRequired]
        public ushort Months { get; set; }
        
        private static readonly DateTime DayCountStarts = new DateTime(2020, 1, 1);
        
        [BsonIgnore]
        public DateTime Date
        {
            get => DayCountStarts.AddMonths(Months);
            set => Months = (ushort) MonthDifference(value,DayCountStarts);
        } 
        private static int MonthDifference(DateTime lValue, DateTime rValue) 
            => (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
    }
}