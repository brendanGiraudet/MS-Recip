using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ms_recip.Data;
using ms_recip.Models;
using ms_recip_tests.Factories;
using ms_recip_tests.Fakers;
using System.Linq.Expressions;

namespace ms_recip_tests.Repositories;

public class BaseRepositoryTests
{
    DatabaseContext _databaseContext;
    ILogger logger => new Mock<ILogger>().Object;

    CategoryModel _categoryModel = new CategoryModelFaker().Generate();

    public BaseRepositoryTests()
    {
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseInMemoryDatabase("test");

        _databaseContext = new DatabaseContext(builder.Options);

        _databaseContext.Database.EnsureDeleted();
        _databaseContext.Database.EnsureCreated();

        _databaseContext.Categories.Add(_categoryModel);

        _databaseContext.SaveChanges();
    }

    #region GetItems
    [Fact]
    public void Should_SuccessResult_And_HaveItems_When_GetItems()
    {
        // Arrange
        var repository = BaseRepositoryFactory<CategoryModel>.CreateBaseRepository(_databaseContext, logger);

        // Act
        var result = repository.GetItems();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Count() > 0);
    }
    #endregion

    #region GetItemAsync
    [Fact]
    public async Task Should_SuccessResult_And_HaveItem_When_GetItemAsync()
    {
        // Arrange
        Expression<Func<CategoryModel, bool>> filterExpression = c => c.Id == _categoryModel.Id;
        var repository = BaseRepositoryFactory<CategoryModel>.CreateBaseRepository(_databaseContext, logger);

        // Act
        var result = await repository.GetItemAsync(filterExpression);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, _categoryModel.Id);
    }
    #endregion

    #region CreateItemAsync
    [Fact]
    public async Task Should_SuccessResult_When_CreateItemAsync()
    {
        // Arrange
        var item = new CategoryModelFaker().Generate();
        var repository = BaseRepositoryFactory<CategoryModel>.CreateBaseRepository(_databaseContext, logger);

        // Act
        var result = await repository.CreateItemAsync(item);

        // Assert
        Assert.True(result.IsSuccess);
    }
    #endregion

    #region UpdateItemAsync
    [Fact]
    public async Task Should_SuccessResult_When_UpdateItemAsync()
    {
        // Arrange
        Expression<Func<CategoryModel, bool>> filterExpression = c => c.Id == _categoryModel.Id;
        var repository = BaseRepositoryFactory<CategoryModel>.CreateBaseRepository(_databaseContext, logger);

        // Act
        var result = await repository.UpdateItemAsync(filterExpression, _categoryModel);

        // Assert
        Assert.True(result.IsSuccess);
    }
    #endregion

    #region DeleteItemAsync
    [Fact]
    public async Task Should_SuccessResult_When_DeleteItemAsync()
    {
        // Arrange
        Expression<Func<CategoryModel, bool>> filterExpression = c => c.Id == _categoryModel.Id;
        var repository = BaseRepositoryFactory<CategoryModel>.CreateBaseRepository(_databaseContext, logger);

        // Act
        var result = await repository.DeleteItemAsync(filterExpression);

        // Assert
        Assert.True(result.IsSuccess);
    }
    #endregion
}
