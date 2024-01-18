using IntelliTect.Coalesce;
using Testcontainers.MsSql;

using IntelliTect.TestContainersDemo.Data;
using System.Security.Claims;


namespace IntelliTect.TestContainersDemo.Tests;

public class DatabaseFixture : IAsyncLifetime, IDisposable
{
    public AppDbContext Db
    {
        get => DbFixture!.DbContext;
        set => DbFixture!.DbContext = Db;
    }

    public CrudContext<AppDbContext>? Context { get; set; }

    private MsSqlContainer SqlContainer;
    private SqlTestDb? DbFixture { get; set; }

    public DatabaseFixture()
    {
        SqlContainer = new MsSqlBuilder().Build();
    }

    public async Task InitializeAsync()
    {
        await SqlContainer.StartAsync();
        DbFixture = new(SqlContainer.GetConnectionString());
        Context = new CrudContext<AppDbContext>(
            Db,
            () => new ClaimsPrincipal()
        );
    }

    public Task DisposeAsync()
    {
        return SqlContainer.DisposeAsync().AsTask();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (disposing)
        {
            DbFixture!.Dispose();
        }
    }
}
