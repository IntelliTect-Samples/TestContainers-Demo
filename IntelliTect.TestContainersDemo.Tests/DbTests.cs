using IntelliTect.TestContainersDemo.Data;
using IntelliTect.TestContainersDemo.Data.Models;
using Xunit.Extensions.Ordering;

namespace IntelliTect.TestContainersDemo.Tests;

public class DbTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    public AppDbContext Db { get => fixture.Db; }

    [Fact, Order(1)]
    public async Task ShouldConnectToDb()
    {
        // Arrange
        Widget widget = new()
        {
            Name = "Thinga-mobab",
            Category = WidgetCategory.Discombobulators,
        };

        // Act
        Db.Widgets.Add(widget);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        // Assert
        Db.Widgets.Single();
    }

    [Fact, Order(2)]
    public async Task ShouldConnectToDbAgain()
    {
        // Arrange
        Widget widget = new()
        {
            Name = "Thinga-mobab",
            Category = WidgetCategory.Discombobulators,
        };

        // Act
        Db.Widgets.Add(widget);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        // Assert
        Assert.Equal(2, Db.Widgets.Count());
    }
}
