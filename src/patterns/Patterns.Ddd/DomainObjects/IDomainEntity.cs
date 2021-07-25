using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Domain
{
    public interface IDomainEntity<TModel> : IDomainObject, IEntity
    {
        void RaiseDomainEvent(IDomainEvent<TModel> domainEvent);
        void ClearDomainEvents();
    }
}