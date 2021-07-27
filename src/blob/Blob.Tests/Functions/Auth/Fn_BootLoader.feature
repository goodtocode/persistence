Feature: Fn Auth Token Check
	Check the validity of an OIDC access token

@health @azureFunction
Scenario: Validate an access token
	Given I have request with a valid access code
	When the access token is validated
	Then the access token is valid
