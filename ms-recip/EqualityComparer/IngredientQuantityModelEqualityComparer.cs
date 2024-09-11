using ms_recip.Models;
using System.Diagnostics.CodeAnalysis;

namespace ms_recip.EqualityComparer;

public class IngredientQuantityModelEqualityComparer : IEqualityComparer<IngredientQuantityModel>
{
    public bool Equals(IngredientQuantityModel? x, IngredientQuantityModel? y)
    {
        return x?.RecipId == y?.RecipId
            && x?.IngredientId == y?.IngredientId;
    }

    public int GetHashCode([DisallowNull] IngredientQuantityModel obj) => obj.GetHashCode();

}
