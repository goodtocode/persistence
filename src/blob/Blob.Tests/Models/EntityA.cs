using System;

namespace GoodToCode.Shared.Blob.Tests
{
    public class EntityA
    {
        public string PartitionKey => "12345";
        public Guid RowKey => new();
        public string SomeData { get; set; }
    }

    public class EntityB
    {
        public string PartitionKey => "67890";
        public Guid RowKey => new();
        public string SomeMoreData { get; set; }
    }
}
