﻿namespace GoodToCode.Shared.Persistence
{
    public interface ICosmosDbServiceConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ContainerName { get; set; }
        string PartitionKeyPath { get; set; }
    }
}
