Feature: TokenContext
	Check functionality of TokenContext class

Scenario: TokenContext validate a bearer token from HttpRequest
	Given I have a TokenContext to a valid AAD app registration
	When a HttpRequest object is validated
	Then the bearer token validation is successful