Feature: Sentiment Analysis by Paragraph
	Analyze a single Paragraph

@cognitiveservices @sentiment
Scenario: Analyze a english Paragraph for positive sentiment
	Given An positive engine Paragraph exists
	When the positive engine Paragraph is analyzed for sentiment
	Then the result of the Paragraph analysys returns a positive sentiment score