using System.Text.Json;
using System.Text.Json.Nodes;
using App.Entities;
using App.Operations;
using App.Operations.AddOutcome;
using App.Operations.AddPEvent;
using App.Operations.AddSampleSpace;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProbSharp.Persistence;

namespace App.Tests.AddEvent.AddAtomicEvent;

public class TestAddAtomicEvent : BaseAppTest
{
    public async override Task InitializeAsync()
    {
        await base.InitializeAsync();
        serviceCollection.RegisterProbSharpApp();
        services = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task AddAtomicEvent_Should_BeSuccessful()
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProbSharpContext>();

        var appOperator = scope.ServiceProvider.GetRequiredService<Operator>();

        var ss = await appOperator.Send(new AddSampleSpaceRequest { Name = "Sample SS" });
        var outcome = await appOperator.Send(new AddOutcomeRequest { Name = "Sample Outcome", SampleSpaceId = ss.Id });
        var atomicEvent = await appOperator.Send(new AddPEventRequest
        {
            Name = "Sample Atomic Event",
            EventType = PEventType.Atomic,
            Outcome = outcome,
            SampleSpaceId = ss.Id,
            Probability = 0.5m
        });

        var insertedAtomicEvent = await dbContext.Nodes.Where(n => n.Id == atomicEvent.Id).FirstOrDefaultAsync();
        insertedAtomicEvent.Should().NotBeNull();
        var attributeJson = JsonNode.Parse(insertedAtomicEvent!.Attributes ?? "{}");
        attributeJson?["Probability"].Should().NotBeNull();
        attributeJson?["Probability"]?.ToString().Should().Be("0.5");

        var relationships = await dbContext.Relationships.Where(r => r.RelatedId == atomicEvent.Id).ToListAsync();
        relationships.Should().Contain(r => r.OwnerId == ss.Id && r.Kind == Constants.RelationshipKind.HasEvent);
        relationships.Should().Contain(r => r.OwnerId == outcome.Id && r.Kind == Constants.RelationshipKind.HasEvent);
    }

}
