using DotNet.Testcontainers.Containers;
using Testcontainers.MsSql;

namespace Customers.Tests;

public sealed class CustomerServiceTest : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
        .Build();

    public Task InitializeAsync()
    {
        return _sqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _sqlContainer.DisposeAsync().AsTask();
    }

    [Fact]
    public void ShouldReturnTwoCustomers()
    {
        // Arrange

        // Act

        // Assert
        Assert.Equal(TestcontainersStates.Running, _sqlContainer.State);
    }
}
