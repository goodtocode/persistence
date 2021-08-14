namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    public class SentimentResult : IConfidence
    {
        public string Text { get; set; }
        public string LanguageIso { get; set; }
        public int Sentiment { get; set; }
        public double Negative { get; set; }
        public double Neutral { get; set; }
        public double Positive { get; set; }

        public SentimentResult(string text, int sentiment, double positive, double neutral, double negative, string languageIso = "en")
        {
            Text = text;
            LanguageIso = languageIso;
            Sentiment = sentiment;
            Positive = positive;
            Neutral = neutral;
            Negative = negative;
        }
    }
}