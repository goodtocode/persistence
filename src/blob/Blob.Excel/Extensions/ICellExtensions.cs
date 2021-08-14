using NPOI.SS.UserModel;
using System;

namespace GoodToCode.Shared.Blob.Excel
{
    public static class ICellExtensions
    {
        public static string ToStringFormatted(this ICell item, IFormulaEvaluator formulaEvaluator = null)
        {
            if (item == null)
                return string.Empty;

            switch (item.CellType)
            {
                case CellType.String:
                    return item.ToString();

                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(item))
                    {
                        try
                        {
                            return item.DateCellValue.ToString();
                        }
                        catch (NullReferenceException)
                        {
                            return DateTime.FromOADate(item.NumericCellValue).ToString();
                        }
                    }
                    return item.NumericCellValue.ToString();


                case CellType.Boolean:
                    return item.BooleanCellValue ? "TRUE" : "FALSE";

                case CellType.Formula:
                    if (formulaEvaluator != null)
                        return ToStringFormatted(formulaEvaluator.EvaluateInCell(item));
                    else
                        return item.CellFormula;

                case CellType.Error:
                    return FormulaError.ForInt(item.ErrorCellValue).String;
                case CellType.Unknown:
                    break;
                case CellType.Blank:
                    break;
            }

            return string.Empty;
        }
    }
}
