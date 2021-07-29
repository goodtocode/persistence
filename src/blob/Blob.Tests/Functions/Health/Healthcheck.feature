Feature: Fn Health Check
	Check health of api via healthchecks

@health @azureFunction
Scenario: Check basic health via Azure Function
	Given I have an Azure Function to check basic health
	When Basic health of the Azure Function is checked
	Then the Basic health of the Azure Function is returned
