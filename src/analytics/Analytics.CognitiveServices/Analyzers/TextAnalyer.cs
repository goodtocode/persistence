using Azure;
using Azure.AI.TextAnalytics;
using GoodToCode.Shared.Analytics.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Analytics.CognitiveServices
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
                returnData = ToSentimentResult(result);
            }
            return returnData;
        }

        public async Task<IList<ISentimentResult>> AnalyzeSentimentBatchAsync(string text)
        {
            List<ISentimentResult> returnData;
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

        public async Task<KeyPhraseResult> ExtractKeyPhrasesAsync(string text)
        {
            var response = await client.ExtractKeyPhrasesAsync(text, await DetectLanguageAsync(text));
            return new KeyPhraseResult(response.Value);
        }

        public async Task<string> DetectLanguageAsync(string text)
        {
            var response = await client.DetectLanguageAsync(text);
            return response.Value.Iso6391Name;
        }

        public async Task<IEnumerable<EntityResult>> ExtractNamedEntitiesAsync(string text)
        {
            var response = await client.RecognizeEntitiesAsync(text, await DetectLanguageAsync(text));
            return response.Value.Select(x => new EntityResult() {Text = x.Text, SubCategory = x.SubCategory, Category = x.Category.ToString(), ConfidenceScore = x.ConfidenceScore });
        }

        public async Task<IEnumerable<HealthcareEntityResult>> ExtractHealthcareEntitiesAsync(string text)
        { 
            List<HealthcareEntityResult> returnData = new List<HealthcareEntityResult>();
            string document1 = text;
            List<string> batchInput = new List<string>()
                {
                    document1,
                    string.Empty
                };
            var options = new AnalyzeHealthcareEntitiesOptions { };
            AnalyzeHealthcareEntitiesOperation healthOperation = await client.StartAnalyzeHealthcareEntitiesAsync(batchInput, "en", options);
            await healthOperation.WaitForCompletionAsync();
            await foreach (AnalyzeHealthcareEntitiesResultCollection documentsInPage in healthOperation.Value)
            {
                foreach (AnalyzeHealthcareEntitiesResult entitiesInDoc in documentsInPage)
                {
                    if (!entitiesInDoc.HasError)
                    {
                        foreach (var entity in entitiesInDoc.Entities)
                        {
                            returnData.Add(new HealthcareEntityResult() { Text = entity.Text, Category = entity.Category.ToString(), SubCategory = entity.SubCategory, Confidence = entity.ConfidenceScore.ToString() });
                        }                        
                    }
                }
            }
            return returnData;
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

        public ISentimentResult ToSentimentResult(DocumentSentiment result)
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