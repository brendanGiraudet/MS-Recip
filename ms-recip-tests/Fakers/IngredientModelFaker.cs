using Bogus;
using ms_recip.Models;

namespace ms_recip_tests.Fakers;

public class IngredientModelFaker : Faker<IngredientModel>
{
    public IngredientModelFaker()
    {
        RuleFor(c => c.Id, f => Guid.NewGuid());
        RuleFor(c => c.Name, f => f.Random.String2(5));
        RuleFor(c => c.Image, f => f.Random.String2(5));
    }
}
