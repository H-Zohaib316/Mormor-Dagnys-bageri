using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MormorBageri.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EShopContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlitedev"));
});

builder.Services.AddControllers();

var app = builder.Build();


app.MapControllers();

app.Run();
