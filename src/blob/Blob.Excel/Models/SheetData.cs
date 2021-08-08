using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Excel
{
    public class SheetData : List<RowData>
    {
        public Guid Id { get; }
        public string PartitionKey { get; }

        public SheetData()
        {

        }
        public SheetData(ISheet sheet)
        {
            for (int count = 0; count < sheet.LastRowNum; count++)
            {
                this.Add(new RowData(sheet.GetRow(count).Cells));
            }
        }
    }
}
