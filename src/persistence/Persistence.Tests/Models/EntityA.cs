
using GoodToCode.Shared.Persistence.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace GoodToCode.Shared.Persistence.Tests
{
    public class EntityA : IEntity
    {
        private string _partitionKey;
        public string PartitionKey { get { return _partitionKey; } set { _partitionKey = value; } }
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string SomeData { get; set; }

        public EntityA() { }
        public EntityA(string partition)
        {
            _partitionKey = partition;
        }
    }

    public class EntityB : IEntity
    {
        private string _partitionKey;
        public string PartitionKey { get { return _partitionKey; } set { _partitionKey = value; } }
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string SomeMoreData { get; set; }

        public EntityB() { }
        public EntityB(string partition)
        {
            _partitionKey = partition;
        }
    }
}
