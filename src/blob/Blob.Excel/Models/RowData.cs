using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Excel
{
    public class RowData : IRowData
    {  
        public RowData()
        {

        }

        public RowData(ICollection<ICell> cells)
        {
            foreach (var item in cells.Select(c => new CellData() { CellValue = c.StringCellValue }))
                this.Cells.Add(item);
        }

        public ICollection<ICellData> Cells { get; set; }

        public int RowIndex { get; set; }

        public string RowKey { get; set; }
    }
}
