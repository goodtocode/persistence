Feature: ExcelBlobReader
	Check functionality of ExcelBloblReader

Scenario: Must read XLSX format
	Given I have an XLSX file
	When read XLSX in via ExcelBlobReader
	Then all readable XLSX data is available to systems

Scenario: Must read XLS format
	Given I have an XLS file
	When read XLS in via ExcelBlobReader
	Then all readable XLS data is available to systems