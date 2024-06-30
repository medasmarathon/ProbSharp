using App.Entities;
using App;
using Microsoft.EntityFrameworkCore;
using ProbSharp.Persistence;
using App.Operations.AddOutcome;
using App.Operations.AddSampleSpace;
using App.Operations.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProbSharpContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseSqlite(builder.Configuration.GetConnectionString("ProbSharp"), b => b.MigrationsAssembly("ProbSharp"));
    else
        options.UseNpgsql(builder.Configuration.GetConnectionString("ProbSharp"), b => b.MigrationsAssembly("ProbSharp"));
});

builder.Services.RegisterProbSharpApp();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var scope = app.Services.CreateScope();
var ssRequest = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddSampleSpaceRequest, SampleSpace>>();
var outcomeRequest = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddOutcomeRequest, Outcome>>();
var ss = await ssRequest.Handle(new AddSampleSpaceRequest { Name = "Sample SS" });
var outcome = await outcomeRequest.Handle(new AddOutcomeRequest { Name = "Sample Outcome", SampleSpaceId = ss.Id });
