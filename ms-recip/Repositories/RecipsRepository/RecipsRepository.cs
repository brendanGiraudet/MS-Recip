using System.Linq.Expressions;
using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.BaseRepository;
using ms_recip.Repositories.RecipCategoriesRepository;

namespace ms_recip.Repositories.RecipsRepository;

public class RecipsRepository(
    DatabaseContext databaseContext,
    ILogger logger,
    IServiceProvider serviceProvider) : BaseRepository<RecipModel>(databaseContext, logger, databaseContext.Recips), IRecipsRepository
{
    public new async Task<MethodResult<RecipModel>> UpdateItemAsync(Expression<Func<RecipModel, bool>> filterExpression, RecipModel model)
    {
        return await UpdateWithCategories(model);
    }

    private async Task<MethodResult<RecipModel>> UpdateWithCategories(RecipModel model)
    {
        var result = await base.UpdateItemAsync(model);

        if(result.IsSuccess)
        {
            IRecipCategoriesRepository recipCategoriesRepository = serviceProvider.GetRequiredService<IRecipCategoriesRepository>();
            await recipCategoriesRepository.SaveItemsAsync(model.Categories ?? [], i => i.RecipId == model.Id);
        }

        return result;
    }

    public new async Task<MethodResult<RecipModel>> UpdateItemAsync(RecipModel model)
    {
        return await UpdateWithCategories(model);
    }
}
