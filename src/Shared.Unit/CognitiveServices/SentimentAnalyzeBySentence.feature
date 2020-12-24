Feature: Sentiment Analysis by Sentence
	Analyze a single sentence

@cognitiveservices @sentiment
Scenario: Analyze a english sentence for positive sentiment
	Given An positive engine sentence exists
	When the positive engine sentence is analyzed for sentiment
	Then the result of the sentence analysys returns a positive sentiment score