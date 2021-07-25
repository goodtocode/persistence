using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Blob.Excel
{
    public class ExcelFileService : IExcelFileService
    {
        public ExcelFileRawDataModel GetFileContent(Stream fileStream)
        {
            IDictionary<string, string> cellValues = new Dictionary<string, string>();
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            ISheet reportSheet = workbook.GetSheetAt(0);

            if (reportSheet != null)
            {
                int rowCount = reportSheet.LastRowNum + 1;
                for (int count = 0; count < rowCount; count++)
                {
                    IRow row = reportSheet.GetRow(count);
                    if (row != null)
                    {
                        foreach (var cell in row.Cells)
                        {
                            var cellValue = cell.GetFormattedCellValue();
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                cellValues.Add(cell.Address.FormatAsString(), cellValue);
                            }
                        }
                    }
                }
            }

            return new ExcelFileRawDataModel()
            {
                Id = cellValues["A6"],
                CarBrand = cellValues["B6"],
                CarModel = cellValues["C6"],
                CarPrice = decimal.Parse(cellValues["D6"]),
                CarAvailability = int.Parse(cellValues["E6"])
            };
        }
    }
}
