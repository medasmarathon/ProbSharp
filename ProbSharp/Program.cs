using App.Entities;
using App.Operations;
using App.Operations.Interfaces;
using App.Operations.Requests;
using Microsoft.EntityFrameworkCore;
using ProbSharp.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProbSharpContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseSqlite(builder.Configuration.GetConnectionString("ProbSharp"), b => b.MigrationsAssembly("ProbSharp"));
    else
        options.UseNpgsql(builder.Configuration.GetConnectionString("ProbSharp"), b => b.MigrationsAssembly("ProbSharp"));
});

builder.Services.AddTransient<IRequestHandler<AddSampleSpaceRequest, SampleSpace>, AddSampleSpaceHandler>();
builder.Services.AddTransient<IRequestHandler<AddOutcomeRequest, Outcome>, AddOutcomeHandler>();
builder.Services.AddTransient<IRequestHandler<AddPEventRequest, PEvent>, AddPEventHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
