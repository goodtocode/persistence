using System;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntity
    {
        Guid id { get; }
        string partitionKey { get; }
    }
}