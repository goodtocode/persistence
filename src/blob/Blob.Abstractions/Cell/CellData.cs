namespace GoodToCode.Shared.Blob.Abstractions
{
    public class CellData : ICellData
    {
        public string CellValue { get; set; }

        public int ColumnIndex { get; set; }

        public string ColumnName { get; set; }

        public int SheetIndex { get; set; }

        public string SheetName { get; set; }

        public string WorkbookName { get; set; }

        public int RowIndex { get; set; }

        public string RowName { get; set; }
    }
}
