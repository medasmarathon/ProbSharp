using App.Entities;
using App.Operations.AddOutcome;
using App.Operations.AddSampleSpace;
using App.Operations.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProbSharp.Persistence;

namespace App.Tests.AddOutcome;

public class TestAddOutcome : BaseDatabaseTest
{
    public TestAddOutcome() : base()
    { 
        serviceCollection.RegisterProbSharpApp();
    }

    [Fact]
    public async Task AddOutcome_Should_BeSuccessful()
    {
        var services = serviceCollection.BuildServiceProvider();
        using var scope = services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ProbSharpContext>();
        dbContext.Database.EnsureCreated();
        var ssRequest = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddSampleSpaceRequest, SampleSpace>>();
        var outcomeRequest = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddOutcomeRequest, Outcome>>();


        var ss = await ssRequest.Handle(new AddSampleSpaceRequest { Name = "Sample SS" });
        var outcome = await outcomeRequest.Handle(new AddOutcomeRequest { Name = "Sample Outcome", SampleSpaceId = ss.Id });

        var insertedSs = await dbContext.Nodes.Where(n => n.Id == ss.Id).FirstOrDefaultAsync();
        var insertedOutcome = await dbContext.Nodes.Where(n => n.Id == outcome.Id).FirstOrDefaultAsync();
        insertedSs.Should().NotBeNull();
        insertedOutcome.Should().NotBeNull();
    }
}
