using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;
using Trabajo_Practico_Final_2026.Data;
using Trabajo_Practico_Final_2026.Filters;
using Trabajo_Practico_Final_2026.Repositories.Implementations;
using Trabajo_Practico_Final_2026.Repositories.Interfaces;
using Trabajo_Practico_Final_2026.Services.Implementations;
using Trabajo_Practico_Final_2026.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.Filters.Add<ApiExceptionFilter>())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddDbContext<MenuContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MenuDb")));
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddSwaggerGen(setupAction =>
{
setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme
{
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    Description = "Acá pegar el token generado al loguearse."
});

setupAction.AddSecurityRequirement(document => new OpenApiSecurityRequirement
{
    [new OpenApiSecuritySchemeReference("ApiBearerAuth", document)] = []
});
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.UseAuthorization(); 

app.MapControllers();

app.Run();
