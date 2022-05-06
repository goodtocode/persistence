﻿using GoodToCode.Persistence.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace GoodToCode.Persistence.Abstractions
{
    public class CellEntity : ICellEntity
    {
        [JsonInclude]
        public string PartitionKey { get; private set; }
        [JsonInclude]
        public string RowKey { get; private set; }
        [JsonInclude]
        public DateTimeOffset? Timestamp { get; set; } = DateTime.UtcNow;
        public string WorkbookName { get; private set; }
        [JsonInclude]
        public string SheetName { get; private set; }
        public int SheetIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        [JsonInclude]
        public string ColumnName { get; private set; }
        [JsonInclude]
        public int RowIndex { get; private set; }
        [JsonInclude]
        public string CellValue { get; private set; }

        public CellEntity() { }

        public CellEntity(string rowKey, ICellData cell)
        {            
            PartitionKey = cell.SheetName;
            RowKey = rowKey;
            WorkbookName = cell.WorkbookName;
            SheetIndex = cell.SheetIndex;
            SheetName = cell.SheetName;
            RowIndex = cell.RowIndex;
            ColumnIndex = cell.ColumnIndex;
            ColumnName = cell.ColumnName;
            CellValue = cell.CellValue;
        }

        public CellEntity(ICellData cell) : this(Guid.NewGuid().ToString(), cell)
        {
        }
    }
}


