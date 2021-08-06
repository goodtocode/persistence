Feature: ConfigurationBuilder
	Check functionality of ConfigurationBuilder class

Scenario: ConfigurationBuilder extension for .AddAzureAppConfiguration
	Given I have a ConfigurationBuilder to a valid AzureAppConfiguration connection
	When a AzureAppConfiguration value is requested
	Then the AzureAppConfiguration value can be evaluated