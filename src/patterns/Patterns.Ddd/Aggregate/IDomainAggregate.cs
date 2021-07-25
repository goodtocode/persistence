using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Domain
{
    public interface IDomainAggregate<TAggregate> : IDomainObject
    {
        IReadOnlyList<IDomainEvent<TAggregate>> DomainEvents { get; }
        void ClearDomainEvents();
    }
}