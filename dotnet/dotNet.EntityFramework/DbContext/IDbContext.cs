﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace GoodToCode.Shared.dotNet.EntityFramework
{
    public interface IDbContext
    {
        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}