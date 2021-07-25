Feature: Caster
	Check functionality of caster

Scenario: Caster must copy data by property name
	Given I have object A to cast to object B by property name
	When Caster is used to cast Object A to Object B
	Then Object B contains the same data from Object A
