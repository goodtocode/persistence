using Microsoft.EntityFrameworkCore;

namespace MySundial.Reflections.Infrastructure
{
    public interface IGenericDbContext<T> where T : class
    {
        DbSet<T> Items { get; set; }

        string GetConnectionFromAzureSettings(string configKey);
    }
}