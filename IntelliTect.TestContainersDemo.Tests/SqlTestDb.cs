using IntelliTect.TestContainersDemo.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntelliTect.TestContainersDemo.Tests;

public class SqlTestDb : IDisposable
{
    public AppDbContext DbContext { get; set; }
    public DbContextOptions<AppDbContext> Options { get; }
    public SqlConnection Connection { get; set; }


    public SqlTestDb(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
        Connection.Open();
        Options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(Connection)
            .UseLoggerFactory(GetLoggerFactory())
            .EnableSensitiveDataLogging()
            .Options;
        DbContext = new(Options);
        DbContext.Database.EnsureCreated();
        DbContext = new(Options);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        DbContext.Dispose();
        Connection.Close();
        Connection.Dispose();
        if (disposing) { }
    }

    private static ILoggerFactory GetLoggerFactory()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging(builder => builder.AddConsole());
        return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>()!;
    }
}
