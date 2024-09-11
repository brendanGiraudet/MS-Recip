using Bogus;
using ms_recip.Models;

namespace ms_recip_tests.Fakers;

public class RecipModelFaker : Faker<RecipModel>
{
    public RecipModelFaker()
    {
        RuleFor(c => c.Id, f => Guid.NewGuid());
        RuleFor(c => c.Name, f => f.Random.String2(5));
        RuleFor(c => c.Image, f => f.Random.String2(5));
        RuleFor(c => c.Authorname, f => f.Person.FirstName);
        RuleFor(c => c.PersonNumber, f => f.Random.Int());
        RuleFor(c => c.CookingDuration, f => f.Date.Soon());
    }
}
