using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ms_recip.Data;
using ms_recip.Models;
using ms_recip_tests.Factories;
using ms_recip_tests.Fakers;
using System.Linq.Expressions;

namespace ms_recip_tests.Repositories;

public class SaveBaseRepositoryTests
{
    DatabaseContext _databaseContext;
    ILogger logger => new Mock<ILogger>().Object;

    CategoryModel _categoryModel = new CategoryModelFaker().Generate();

    public SaveBaseRepositoryTests()
    {
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseInMemoryDatabase("test");

        _databaseContext = new DatabaseContext(builder.Options);

        _databaseContext.Database.EnsureDeleted();
        _databaseContext.Database.EnsureCreated();

        var recips = new RecipModelFaker().Generate(5);
        _databaseContext.Recips.AddRange(recips);

        var ingredients = new IngredientModelFaker().Generate(5);
        _databaseContext.Ingredients.AddRange(ingredients);

        _databaseContext.SaveChanges();
    }

    #region SaveItemsAsync
    [Fact]
    public async Task Should_SuccessResult_When_SaveItemsAsync()
    {
        // Arrange
        var recip = _databaseContext.Recips.First();
        var ingredient = _databaseContext.Ingredients.First();
        Expression<Func<IngredientQuantityModel, bool>> filterExpression = c => c.RecipId == recip.Id;
        var repository = SaveBaseRepositoryFactory<IngredientQuantityModel>.CreateSaveBaseRepository(_databaseContext, logger);

        var items = new List<IngredientQuantityModel>(){
            new IngredientQuantityModel()
            {
                Quantity=10,
                IngredientId = ingredient.Id,
                RecipId = recip.Id,
                MeasureUnit = "unit"
            }
        };

        // Act
        var result = await repository.SaveItemsAsync(items, filterExpression);

        // Assert
        Assert.True(result.IsSuccess);
    }
    #endregion
}
