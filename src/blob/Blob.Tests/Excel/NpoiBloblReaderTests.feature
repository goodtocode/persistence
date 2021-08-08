Feature: NpoiBlobReader
	Check functionality of ExcelBloblReader

Scenario: Must read XLSX format via Npoi
	Given I have an XLSX file and Npoi
	When read XLSX in via NpoiBlobReader
	Then all Npoi readable XLSX data is available to systems

Scenario: Must read XLS format via Npoi
	Given I have an XLS file and Npoi
	When read XLS in via NpoiBlobReader
	Then all Npoi readable XLS data is available to systems