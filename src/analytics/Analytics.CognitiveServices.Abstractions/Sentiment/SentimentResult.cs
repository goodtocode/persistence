using System.Globalization;

namespace GoodToCode.Shared.Analytics.Abstractions
{
    /// <summary>
    /// Abstraction of Azure.AI.TextAnalytics.TextSentiment
    /// </summary>
    public class SentimentResult : ISentimentResult
    {
        public string Text { get; } = string.Empty;

        public string LanguageIso { get; } = "en-US";

        public int Sentiment { get; } = (int)SentimentScore.Neutral;

        public double Negative { get; }

        public double Neutral { get; }

        public double Positive { get; }

        public SentimentResult(string text, int sentiment, double positive, double neutral, double negative, string language)
        {
            Positive = positive;
            Neutral = neutral;
            Negative = negative;
            Text = text;
            Sentiment = sentiment;
            LanguageIso = language;
        }

        public SentimentResult(string text, int sentiment, double positive, double neutral, double negative) : this(text, sentiment, positive, neutral, negative, "en-US")
        {
        }

        public SentimentResult(string text, string language, int sentiment, IConfidence confidence) : this(text, sentiment, confidence.Positive, confidence.Neutral, confidence.Negative, language)
        {
        }
    }
}
