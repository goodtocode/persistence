﻿using Microsoft.EntityFrameworkCore;

namespace GoodToCode.Shared.Repository
{
    public interface IGenericDbContext<T> where T : class
    {
        DbSet<T> Items { get; set; }

        string GetConnectionFromAzureSettings(string configKey);
    }
}