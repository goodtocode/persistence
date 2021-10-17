using System;

namespace GoodToCode.Shared.Blob.Unit.Tests
{
    public class EntityA
    {
        public static string PartitionKey => "12345";
        public static Guid RowKey => new();
        public string SomeData { get; set; }
    }

    public class EntityB
    {
        public static string PartitionKey => "67890";
        public static Guid RowKey => new();
        public string SomeMoreData { get; set; }
    }
}
