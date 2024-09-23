namespace ms_recip.Constants;

public static class RabbitmqConstants
{
    public const string RecipExchangeName = "recip";

    public const string ApplicationName = "ms-recip";

    // RECIP
    public const string CreateRecipRoutingKey = "CreateRecip";
    public const string CreateRecipResultRoutingKey = "CreateRecipResult";
    
    public const string UpdateRecipRoutingKey = "UpdateRecip";
    public const string UpdateRecipResultRoutingKey = "UpdateRecipResult";
    
    public const string DeleteRecipRoutingKey = "DeleteRecip";
    public const string DeleteRecipResultRoutingKey = "DeleteRecipResult";
    
    // INGREDIENT
    public const string CreateIngredientRoutingKey = "CreateIngredient";
    public const string CreateIngredientResultRoutingKey = "CreateIngredientResult";
    
    public const string UpdateIngredientRoutingKey = "UpdateIngredient";
    public const string UpdateIngredientResultRoutingKey = "UpdateIngredientResult";
    
    public const string DeleteIngredientRoutingKey = "DeleteIngredient";
    public const string DeleteIngredientResultRoutingKey = "DeleteIngredientResult";
    
    // CATEGORY
    public const string CreateCategoryRoutingKey = "CreateCategory";
    public const string CreateCategoryResultRoutingKey = "CreateCategoryResult";
    
    public const string UpdateCategoryRoutingKey = "UpdateCategory";
    public const string UpdateCategoryResultRoutingKey = "UpdateCategoryResult";
    
    public const string DeleteCategoryRoutingKey = "DeleteCategory";
    public const string DeleteCategoryResultRoutingKey = "DeleteCategoryResult";
}
