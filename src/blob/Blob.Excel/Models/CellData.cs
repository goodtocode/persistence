namespace GoodToCode.Shared.Blob.Excel
{
    public class CellData : ICellData
    {
        public string CellValue { get; set; }

        public int ColumnIndex { get; set; }

        public string ColumnKey { get; set; }

        public int SheetIndex { get; set; }

        public string SheetKey { get; set; }

        public int RowIndex { get; set; }

        public string RowKey { get; set; }
    }
}
