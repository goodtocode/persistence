
using GoodToCode.Shared.Persistence.Abstractions;
using System;

namespace GoodToCode.Shared.Persistence.Tests
{
    public class EntityA : IEntity
    {
        private string _partitionKey;
        public string partitionKey { get { return _partitionKey; } }
        public Guid id => new();
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
        public string partitionKey { get { return _partitionKey; } }
        public Guid id => new();
        public string SomeMoreData { get; set; }

        public EntityB() { }
        public EntityB(string partition)
        {
            _partitionKey = partition;
        }
    }
}
