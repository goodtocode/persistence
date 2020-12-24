using Azure;
using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoodToCode.Shared.CognitiveServices
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly AzureKeyCredential credentials;
        private readonly TextAnalyticsClient client;

        public TextAnalyzer(string keyCredential, Uri endpoint)
        {
            credentials = new AzureKeyCredential(keyCredential);
            client = new TextAnalyticsClient(endpoint, credentials);
        }

        public async Task<ISentimentResult> AnalyzeSentimentAsync(string text)
        {
            return await AnalyzeSentimentAsync(text, await DetectLanguageAsync(text));
        }

        public async Task<ISentimentResult> AnalyzeSentimentAsync(string text, string languageIso)
        {
            ISentimentResult returnData = new SentimentResult();
            if (text.Length > 0)
            {
                var result = await client.AnalyzeSentimentAsync(text, languageIso);
                returnData = ToSentimentResult(result, languageIso);
            }
            return returnData;
        }

        public async Task<IList<ISentimentResult>> AnalyzeSentimentBatchAsync(string text)
        {
            List<ISentimentResult> returnData = new List<ISentimentResult>();
            AnalyzeSentimentResultCollection results = null;
            var sentences = SplitParagraph(text);
            string language = "en-US";
            if (sentences.Length > 0)
            {                
                string first = sentences[0];
                language = await DetectLanguageAsync(first);
                results = await client.AnalyzeSentimentBatchAsync(sentences, language);
            }

            returnData = ToSentimentResult(results, language);

            return returnData;
        }

        public async Task<IList<string>> ExtractKeyPhrasesAsync(string text)
        {
            var response = await client.ExtractKeyPhrasesAsync(text, await DetectLanguageAsync(text));
            return response.Value;
        }

        public async Task<string> DetectLanguageAsync(string text)
        {
            var response = await client.DetectLanguageAsync(text);
            return response.Value.Iso6391Name;
        }

        public List<ISentimentResult> ToSentimentResult(AnalyzeSentimentResultCollection results, string languageIso)
        {
            List<ISentimentResult> returnValue = new List<ISentimentResult>();

            foreach(var item in results)
            {
                returnValue.AddRange(item.DocumentSentiment.Sentences.Select(x => new SentimentResult()
                {
                    Text = x.Text,
                    LanguageIso = languageIso,
                    Sentiment = (int)x.Sentiment,
                    Confidence = new SentimentConfidence() { Positive = x.ConfidenceScores.Positive, Neutral = x.ConfidenceScores.Neutral, Negative = x.ConfidenceScores.Negative }
                }));
            }
           return returnValue;
        }

        public ISentimentResult ToSentimentResult(DocumentSentiment result, string languageIso)
        {
            
            return new SentimentResult()
            {
                Text = result.Sentences.Count > 0 ? result.Sentences.ElementAt(0).Text : string.Empty,
                Sentiment = (int)result.Sentiment,
                Confidence = new SentimentConfidence
                {
                    Positive = result.ConfidenceScores.Positive,
                    Negative = result.ConfidenceScores.Negative,
                    Neutral = result.ConfidenceScores.Neutral
                }
            };
        }

        public string[] SplitParagraph(string paragraph)
        {
            return Regex.Split(paragraph, @"(?<=[\.!\?])\s+");
        }
    }
}