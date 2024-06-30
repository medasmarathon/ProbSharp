using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProbSharp.Persistence;

namespace App.Tests
{
    public abstract class BaseDatabaseTest
    {
        protected IServiceCollection serviceCollection;
        public BaseDatabaseTest()
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ProbSharpContext>(options =>
                options.UseSqlite($"Data Source={this.GetType().Name};Mode=Memory", builder => builder.MigrationsAssembly("App.Tests"))
            );
        }
    }
}