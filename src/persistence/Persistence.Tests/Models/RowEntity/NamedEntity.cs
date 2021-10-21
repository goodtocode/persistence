﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GoodToCode.Shared.Persistence.Tests
{
    public class NamedEntity : RowEntity, INamedEntity
    {

        [JsonInclude]
        public string Category { get; private set; }
        [JsonInclude]
        public string SubCategory { get; private set; }
        [JsonInclude]
        public double Confidence { get; private set; }
        [JsonInclude]
        public string AnalyzedText { get; private set; }

        public NamedEntity() { }

        public NamedEntity(ICellData cell, IAnalyticsResult result) : base(Guid.NewGuid().ToString(), new List<ICellData>() { cell })
        {
            AnalyzedText = result.AnalyzedText;
            Category = result.Category;
            SubCategory = result.SubCategory;
            Confidence = result.Confidence;
        }
    }
}


