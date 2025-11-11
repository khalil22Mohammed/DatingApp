using FirstApp.Data;
using FirstApp.Entities;
using FirstApp.Interfaces;
using FirstApp.Migrations;
using FirstApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql =>
        {
            sql.EnableRetryOnFailure(3);
        })
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine)
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddControllers();
builder.Services.AddScoped<ItokenService,TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenkey = builder.Configuration["TokenKey"] ?? throw new Exception(" Connot Get Token key");
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

    });
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(); // generate swagger JSON
app.UseSwaggerUI(); // interactive UI

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}
app.Run();
