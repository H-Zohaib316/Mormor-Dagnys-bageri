using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MormorBageri.Data;
using MormorBageri.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EShopContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlitedev"));
    //options.UseNpgsql(builder.Configuration.GetConnectionString("postgresdev"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EShopContext>();

    // Se till att databasen finns
    await context.Database.MigrateAsync();

    // Kör seed
    // await DbSeedData.SeedAsync(context);
}

app.MapControllers();

app.Run();
