using Azure;
using Azure.AI.TextAnalytics;
using GoodToCode.Shared.Analytics.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    public class TextAnalyzerService : ITextAnalyzerService
    {
        private readonly ICognitiveServiceConfiguration config;
        private readonly AzureKeyCredential credentials;
        private readonly TextAnalyticsClient client;

        public TextAnalyzerService(ICognitiveServiceConfiguration serviceConfiguration)
        {
            config = serviceConfiguration;
            credentials = new AzureKeyCredential(config.KeyCredential);
            client = new TextAnalyticsClient(new Uri(config.Endpoint), credentials);
        }

        public TextAnalyzerService(CognitiveServiceOptions options) : this(options.Value)
        {
        }


        public async Task<ISentimentResult> AnalyzeSentimentAsync(string text)
        {
            return await AnalyzeSentimentAsync(text, await DetectLanguageAsync(text));
        }

        public async Task<ISentimentResult> AnalyzeSentimentAsync(string text, string languageIso)
        {
            ISentimentResult returnData = null;
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

        public async Task<KeyPhrases> ExtractKeyPhrasesAsync(string text)
        {
            var response = await client.ExtractKeyPhrasesAsync(text, await DetectLanguageAsync(text));
            return new KeyPhrases(response.Value);
        }

        public async Task<LinkedResult> ExtractEntityLinksAsync(string text)
        {
            var response = await client.RecognizeLinkedEntitiesAsync(text);

            var returnData = response.Value.Select(x => new LinkedMatches() { Name = x.Name, Url = x.Url.ToString(), Matches = string.Join(@"\n", x.Matches.Select(m => m.Text).ToArray()) });
            return new LinkedResult(returnData.ToList());
        }

        public async Task<IEnumerable<OpinionResult>> ExtractOpinionAsync(string text)
        {
            var returnData = new List<OpinionResult>();

            var documents = new List<string>
                {
                    text
                };

            AnalyzeSentimentResultCollection reviews = await client.AnalyzeSentimentBatchAsync(documents, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });


            foreach (AnalyzeSentimentResult review in reviews)
            {

                foreach (SentenceSentiment sentence in review.DocumentSentiment.Sentences)
                {
                    foreach (SentenceOpinion sentenceOpinion in sentence.Opinions)
                    {
                        foreach (AssessmentSentiment assessment in sentenceOpinion.Assessments)
                        {

                            returnData.Add(
                                new OpinionResult(
                                    new SentimentConfidence(review.DocumentSentiment.ConfidenceScores.Positive, review.DocumentSentiment.ConfidenceScores.Negative, review.DocumentSentiment.ConfidenceScores.Neutral),
                                    new SentimentResult(sentence.Text, sentence.Sentiment, sentence.ConfidenceScores.Positive, sentence.ConfidenceScores.Negative, sentence.ConfidenceScores.Neutral),
                                    new SentimentResult(sentenceOpinion.Target.Text, sentence.Sentiment, sentenceOpinion.Target.ConfidenceScores.Positive, sentenceOpinion.Target.ConfidenceScores.Negative, sentenceOpinion.Target.ConfidenceScores.Neutral),
                                    new SentimentResult(assessment.Text, assessment.Sentiment, assessment.ConfidenceScores.Positive, assessment.ConfidenceScores.Negative, assessment.ConfidenceScores.Neutral)
                                    ));
                        }
                    }
                }
            }
            return returnData;
        }

        public async Task<string> DetectLanguageAsync(string text)
        {
            var response = await client.DetectLanguageAsync(text);
            return response.Value.Iso6391Name;
        }

        public async Task<IEnumerable<EntityResult>> ExtractEntitiesAsync(string text)
        {
            var response = await client.RecognizeEntitiesAsync(text, await DetectLanguageAsync(text));
            return response.Value.Select(x => new EntityResult() { AnalyzedText = x.Text, SubCategory = x.SubCategory, Category = x.Category.ToString(), Confidence = x.ConfidenceScore });
        }

        public async Task<IEnumerable<IAnalyticsResult>> ExtractHealthcareEntitiesAsync(string text)
        {
            List<IAnalyticsResult> returnData = new List<IAnalyticsResult>();
            string document1 = text;
            List<string> batchInput = new List<string>()
                {
                    document1,
                    string.Empty
                };
            var options = new AnalyzeHealthcareEntitiesOptions { };
            AnalyzeHealthcareEntitiesOperation healthOperation = await client.StartAnalyzeHealthcareEntitiesAsync(batchInput, "en-US", options);
            await healthOperation.WaitForCompletionAsync();
            await foreach (AnalyzeHealthcareEntitiesResultCollection documentsInPage in healthOperation.Value)
            {
                foreach (AnalyzeHealthcareEntitiesResult entitiesInDoc in documentsInPage)
                {
                    if (!entitiesInDoc.HasError)
                    {
                        foreach (var entity in entitiesInDoc.Entities)
                        {
                            returnData.Add(new HealthcareEntityResult() { AnalyzedText = entity.Text, Category = entity.Category.ToString(), SubCategory = entity.SubCategory, Confidence = entity.ConfidenceScore });
                        }
                    }
                }
            }
            return returnData;
        }

        public List<ISentimentResult> ToSentimentResult(AnalyzeSentimentResultCollection results, string languageIso)
        {
            List<ISentimentResult> returnValue = new List<ISentimentResult>();
            foreach (var item in results)
            {
                returnValue.AddRange(item.DocumentSentiment.Sentences.Select(x => new SentimentResult(
                    text: x.Text,
                    language: languageIso,
                    sentiment: x.Sentiment,
                    positive: x.ConfidenceScores.Positive,
                    neutral: x.ConfidenceScores.Neutral,
                    negative: x.ConfidenceScores.Negative
                )));
            }
            return returnValue;
        }

        public ISentimentResult ToSentimentResult(DocumentSentiment result)
        {

            return new SentimentResult(
                text: result.Sentences.Count > 0 ? result.Sentences.ElementAt(0).Text : string.Empty,
                sentiment: result.Sentiment,
                positive: result.ConfidenceScores.Positive,
                negative: result.ConfidenceScores.Negative,
                neutral: result.ConfidenceScores.Neutral
            );
        }

        public string[] SplitParagraph(string paragraph)
        {
            return Regex.Split(paragraph, @"(?<=[\.!\?])\s+");
        }




    }
}
