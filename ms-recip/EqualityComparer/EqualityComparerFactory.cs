using ms_recip.Models;

namespace ms_recip.EqualityComparer;

public static class EqualityComparerFactory<T>
{
    public static IEqualityComparer<T>? CreateEqualityComparer()
    {
        var typeName = typeof(T).Name;

        IEqualityComparer<T>? equalityComparer = typeName switch
        {
            nameof(IngredientQuantityModel) => new IngredientQuantityModelEqualityComparer() as IEqualityComparer<T>,
            _ => null
        };

        return equalityComparer;
    }
}
