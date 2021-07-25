Feature: Converter
	Check functionality of converter

Scenario: Converter ToDictionary must create a dictionary from strings
	Given I have two strings
	When Converter ToDictionary is called
	Then A dictionary object of those strings is returned
