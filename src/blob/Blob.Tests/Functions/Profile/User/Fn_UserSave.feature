Feature: Fn User Save
	Save a new and existing user to persistence

@command @azureFunction
Scenario: Create a new user via Azure Function
	Given I have a new user for the Azure Function
	When User is created via Azure Function
	Then the user is inserted to persistence from the Azure Function
