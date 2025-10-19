using FirstApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(); // generate swagger JSON
app.UseSwaggerUI(); // interactive UI
app.MapControllers();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:52693", "https://localhost:52693"));
app.Run();
