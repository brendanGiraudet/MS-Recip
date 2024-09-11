using Bogus;
using ms_recip.Models;

namespace ms_recip_tests.Fakers;

public class CategoryModelFaker : Faker<CategoryModel>
{
    public CategoryModelFaker()
    {
        RuleFor(c => c.Id, f => Guid.NewGuid());
        RuleFor(c => c.Name, f => f.Random.String2(5));
    }
}
