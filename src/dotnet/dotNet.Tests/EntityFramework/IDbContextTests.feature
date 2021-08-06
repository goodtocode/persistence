Feature: IDbContext
	Check functionality of IDbContext interface

Scenario: IDbContext interface abstracts DbContext
	Given I have a IDbContext abstraction to DbContext
	When a IDbContext is interrogated for functionality
	Then the IDbContext contains abstracted DbContext functionality