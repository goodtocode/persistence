Feature: HttpRequestExtensions
	Check functionality of HttpRequestExtensions class

Scenario: HttpRequestExtensions extension for .ToUri
	Given I have a HttpRequestExtensions to a valid HttpRequest object
	When a ToUri method is requested
	Then the ToUri Uri value can be evaluated