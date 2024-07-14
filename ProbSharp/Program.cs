using App;
using Microsoft.EntityFrameworkCore;
using ProbSharp.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProbSharpContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseSqlite(builder.Configuration.GetConnectionString("ProbSharp"), b => b.MigrationsAssembly("ProbSharp.Persistence"));
    else
        options.UseNpgsql(builder.Configuration.GetConnectionString("ProbSharp"), b => b.MigrationsAssembly("ProbSharp.Persistence"));
});

builder.Services.RegisterProbSharpApp();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

