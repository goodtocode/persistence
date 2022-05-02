using GoodToCode.Shared.Persistence.Abstractions;
using System.Collections.Generic;

namespace GoodToCode.Persistence.Tests
{
    public interface IWorkbookMetadata
    {
        string WorkbookName { get; }
    }

    public interface ISheetMetadata : IWorkbookMetadata
    {
        int SheetIndex { get; }
        string SheetName { get; }
    }

    public interface IColumnMetadata
    {
        int ColumnIndex { get; }
        string ColumnName { get; }
    }

    public interface ICellMetadata : ISheetMetadata, IWorkbookMetadata, IColumnMetadata, IRowMetadata
    {
    }

    public interface ICellData : ICellMetadata, ISheetMetadata, IWorkbookMetadata, IColumnMetadata, IRowMetadata
    {
        string CellValue { get; }
    }

    public interface IRowMetadata
    {
        int RowIndex { get; }
    }

    public interface IRowData : IRowMetadata
    {
        IEnumerable<ICellData> Cells { get; }

        Dictionary<string, object> ToDictionary();
    }

    public interface IRowEntity : IRowData, IEntity
    {

    }
}
