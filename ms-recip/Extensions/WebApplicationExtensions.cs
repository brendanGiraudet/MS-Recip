using Microsoft.EntityFrameworkCore;
using ms_recip.Data;

namespace ms_recip.Extensions;

public static class WebApplicationExtensions
{
    public static void ApplyDatabaseMigrations(this WebApplication webApplication)
    {
        using var serviceScope = webApplication.Services.GetService<IServiceScopeFactory>()?.CreateScope();

        serviceScope?.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
    }
}