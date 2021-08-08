namespace GoodToCode.Shared.Blob.Abstractions
{
    public class CellData : ICellData
    {
        public string CellValue { get; set; }
        public int ColumnIndex { get; set; }
        public string SheetKey { get; set; }
        public int RowIndex { get; set; }
        public string WorkbookKey { get; set; }
    }
}
