using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProbSharp.Persistence;

namespace App.Tests
{
    public abstract class BaseAppTest : IAsyncLifetime
    {
        public IServiceCollection serviceCollection = new ServiceCollection();
        public ServiceProvider services;
        public void SetupDatabase()
        {
            serviceCollection.AddDbContext<ProbSharpContext>(
                options => options
                    .UseSqlite($"DataSource={this.GetType().Name}.db", opt => opt.MigrationsAssembly("ProbSharp.Persistence"))
            );
        }

        public virtual Task InitializeAsync()
        {
            SetupDatabase();
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));
            
            var dbContext = services.GetRequiredService<ProbSharpContext>();
            dbContext.Database.EnsureDeleted();
            await services.DisposeAsync();
            
            FileInfo fi = new(this.GetType().Name + ".db");
            if (fi.Exists)
                fi.Delete();
            return;
        }
    }
}