using Microsoft.EntityFrameworkCore;
using ms_recip.Models;

namespace ms_recip.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<RecipModel> Recips { get; set; }
    public DbSet<RecipStepModel> RecipSteps { get; set; }
    public DbSet<IngredientModel> Ingredients { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<ProfilModel> Profils { get; set; }
    public DbSet<UserModel> Users { get; set; }
}
