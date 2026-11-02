using Microsoft.EntityFrameworkCore;
using Trabajo_Practico_Final_2026.Data;
using Trabajo_Practico_Final_2026.Repositories.Implementations;
using Trabajo_Practico_Final_2026.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<MenuContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MenuDb")));
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
