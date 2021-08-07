Feature: CosmosDbService
	Check functionality of CosmosDbService

Scenario: Must read CosmosDb from service
	Given I have an CosmosDbService for reading
	When read a record via CosmosDbService
	Then all the CosmosDbService record contains the expected data

Scenario: Must write to the CosmosDb service
	Given I have an CosmosDbService for writing
	When write a record via CosmosDbService
	Then the record can be read back from CosmosDbService