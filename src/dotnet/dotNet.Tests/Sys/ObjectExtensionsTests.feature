Feature: ObjectExtensions
	Check functionality of ObjectExtensions

Scenario: Must be able to fill Object A with Object B data
	Given I have object A to Fill to object B by property name
	When Fill is used to cast Object A to Object B
	Then Object B is Filled with the same data from Object A
