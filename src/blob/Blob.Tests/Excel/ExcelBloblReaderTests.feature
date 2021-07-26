Feature: ExcelBlobReader
	Check functionality of ExcelBloblReader

Scenario: Must be able to read from local file in XLSX format
	Given I have an XLSX file
	When read XLSX in via ExcelBlobReader
	Then all readable XLSX data is available to systems

Scenario: Must be able to read from local file in XLS format
	Given I have an XLS file
	When read XLS in via ExcelBlobReader
	Then all readable XLS data is available to systems

Scenario: Must be able to read from local file in CSV format
	Given I have an CSV file
	When read CSV in via ExcelBlobReader
	Then all readable CSV data is available to systems