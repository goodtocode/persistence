namespace GoodToCode.Shared.Analytics.Abstractions
{
    public class OpinionResult
    {
        public IConfidence DocumentSentiment { get; set; }
        public ISentimentResult SentenceSentiment { get; set; }
        public ISentimentResult SentenceOpinion { get; set; }
        public ISentimentResult OpinionSentiments { get; set; }

        public OpinionResult(IConfidence document, ISentimentResult sentence, ISentimentResult opinion, ISentimentResult sentiment)
        {
            DocumentSentiment = document;
            SentenceSentiment = sentence;
            SentenceOpinion = opinion;
            OpinionSentiments = sentiment;
        }
   }
}
