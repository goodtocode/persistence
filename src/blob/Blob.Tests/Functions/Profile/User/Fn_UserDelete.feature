Feature: Fn User Delete
	Delete a user from persistence

@command @webapi
Scenario: Delete an existing user via Azure Function
	Given I have an non empty user key for the Azure Function to delete
		And the user key is provided for the Azure Function
	When User is deleted via Azure Function
	Then the user does not exist in persistence when queried from Azure Function