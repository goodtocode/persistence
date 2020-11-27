using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Domain
{
    public interface IDomainEntity<TModel> : IDomainObject
    {
        Guid RowKey { get; }
        string PartitionKey { get; }
        void RaiseDomainEvent(IDomainEvent<TModel> domainEvent);
        void ClearDomainEvents();
    }
}