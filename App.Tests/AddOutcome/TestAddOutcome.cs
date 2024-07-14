using App.Entities;
using App.Operations;
using App.Operations.AddOutcome;
using App.Operations.AddSampleSpace;
using App.Operations.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProbSharp.Persistence;

namespace App.Tests.AddOutcome;

public class TestAddOutcome : BaseAppTest
{
    public async override Task InitializeAsync()
    {
        await base.InitializeAsync();
        serviceCollection.RegisterProbSharpApp();
        services = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task AddOutcome_Should_BeSuccessful()
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProbSharpContext>();

        var appOperator = scope.ServiceProvider.GetRequiredService<Operator>();

        var ss = await appOperator.Send(new AddSampleSpaceRequest { Name = "Sample SS" });
        var outcome = await appOperator.Send(new AddOutcomeRequest { Name = "Sample Outcome", SampleSpaceId = ss.Id });

        var insertedSs = await dbContext.Nodes.Where(n => n.Id == ss.Id).FirstOrDefaultAsync();
        var insertedOutcome = await dbContext.Nodes.Where(n => n.Id == outcome.Id).FirstOrDefaultAsync();
        insertedSs.Should().NotBeNull();
        insertedOutcome.Should().NotBeNull();
    }

}
