﻿
using GoodToCode.Shared.Workflows.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace GoodToCode.Shared.Workflows.Tests
{
    public class EntityA : IEntity
    {
        private string _partitionKey;
        public string PartitionKey { get { return _partitionKey; } set { _partitionKey = value; } }
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string SomeString { get; set; }
        public int SomeNumber { get; set; } = 2;

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
        public string AnotherString { get; set; }
        public int AnotherNumber { get; set; } = 5;

        public EntityB() { }
        public EntityB(string partition)
        {
            _partitionKey = partition;
        }
    }
}
