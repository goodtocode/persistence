
using GoodToCode.Shared.Persistence.Abstractions;
using System;

namespace GoodToCode.Shared.Persistence.Tests
{
    public class EntityA : IEntity
    {        
        public string PartitionKey => "12345";

        public Guid Id => new();
        public string SomeData { get; set; }
    }

    public class EntityB : IEntity
    {
        public string PartitionKey => "67890";

        public Guid Id => new();
        public string SomeMoreData { get; set; }
    }
}
