using System;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MvcWebshop.Data;
using Webshop.Controllers;
using Xunit;

namespace TestWebshop;

public class UnitTest1
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<MvcWebshopContext> _contextOptions;

    #region ConstructorAndDispose
    public UnitTest1()
    {
        // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
        // at the end of the test (see Dispose below).
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        // These options will be used by the context instances in this test suite, including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<MvcWebshopContext>()
            .UseSqlite(_connection)
            .Options;

        // Create the schema and seed some data
        using var context = new MvcWebshopContext(_contextOptions);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
CREATE VIEW AllResources AS
SELECT Url
FROM Blogs;";
            viewCommand.ExecuteNonQuery();
        }

        context.AddRange(
            new Product { Name = "Product1", Description = "Product1", Price = 1, Image = "Image1" },
            new Product { Name = "Product2", Description = "Product2", Price = 1, Image = "Image2" }
        );
        context.SaveChanges();
    }
    MvcWebshopContext CreateContext() => new MvcWebshopContext(_contextOptions);

    public void Dispose() => _connection.Dispose();
    #endregion


    [Fact]
    async public void Test1()
    {
        using var context = CreateContext();

        var controller = new ProductsController(context);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Collection(
            Assert.IsType<List<Product>>(viewResult.Model),
            item => Assert.Equal("Product1", item.Name),
            item => Assert.Equal("Product2", item.Name)
        );
        Assert.True(true);
    }
}