﻿using System.Globalization;

namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    /// <summary>
    /// Abstraction of Azure.AI.TextAnalytics.TextSentiment
    /// </summary>
    public class SentimentResult : ISentimentResult
    {
        public string Text { get; set; } = string.Empty;
        public string LanguageIso { get; set; } = "en-US";
        public int Sentiment { get; set; } = (int)SentimentValues.Neutral;
        public IConfidence Confidence { get; set; } = new SentimentConfidence();        
    }
}
