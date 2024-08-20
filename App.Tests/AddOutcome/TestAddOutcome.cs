using App.Operations.AddOutcome;
using App.Operations.AddSampleSpace;
using FluentAssertions;
using Mediator;
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

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var ss = await mediator.Send(new AddSampleSpaceRequest { Name = "Sample SS" });
        var outcome = await mediator.Send(new AddOutcomeRequest { Name = "Sample Outcome", SampleSpaceId = ss.Id });

        var insertedSs = await dbContext.Nodes.Where(n => n.Id == ss.Id).FirstOrDefaultAsync();
        var insertedOutcome = await dbContext.Nodes.Where(n => n.Id == outcome.Id).FirstOrDefaultAsync();
        insertedSs.Should().NotBeNull();
        insertedOutcome.Should().NotBeNull();
    }

}
