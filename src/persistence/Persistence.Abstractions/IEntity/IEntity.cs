﻿using System;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntity
    {
        string id { get; }
        string PartitionKey { get; }
    }
}