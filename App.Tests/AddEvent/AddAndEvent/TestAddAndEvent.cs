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

namespace App.Tests.AddEvent.AddAndEvent;

public class TestAddAndEvent : BaseAppTest
{
    public async override Task InitializeAsync()
    {
        await base.InitializeAsync();
        serviceCollection.RegisterProbSharpApp();
        services = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task AddAndEvent_Should_BeSuccessful()
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProbSharpContext>();

        var appOperator = scope.ServiceProvider.GetRequiredService<Operator>();

        var ss = await appOperator.Send(new AddSampleSpaceRequest { Name = "Sample SS" });
        var outcome1 = await appOperator.Send(new AddOutcomeRequest { Name = "Outcome 1", SampleSpaceId = ss.Id });
        var outcome2 = await appOperator.Send(new AddOutcomeRequest { Name = "Outcome 2", SampleSpaceId = ss.Id });
        var event1 = await appOperator.Send(new AddPEventRequest
        {
            Name = "Event 1",
            EventType = PEventType.Atomic,
            Outcome = outcome1,
            SampleSpaceId = ss.Id,
            Probability = 0.5m
        });
        var event2 = await appOperator.Send(new AddPEventRequest
        {
            Name = "Event 2",
            EventType = PEventType.Atomic,
            Outcome = outcome2,
            SampleSpaceId = ss.Id,
            Probability = 0.5m
        });
        var andEvent = await appOperator.Send(new AddPEventRequest
        {
            Name = "And Event",
            EventType = PEventType.And,
            SubEvents = [event1, event2],
            Probability = 0.2m,
            SampleSpaceId = ss.Id
        });

        var insertedAndEvent = await dbContext.Nodes.Where(n => n.Id == andEvent.Id).FirstOrDefaultAsync();
        insertedAndEvent.Should().NotBeNull();
        var attributeJson = JsonNode.Parse(insertedAndEvent!.Attributes ?? "{}");
        attributeJson?["Probability"].Should().NotBeNull();
        attributeJson?["Probability"]?.ToString().Should().Be("0.2");

        var relatedRelationships = await dbContext.Relationships.Where(r => r.RelatedId == andEvent.Id).ToListAsync();
        relatedRelationships.Should().Contain(r => 
            r.OwnerId == ss.Id && 
            r.Kind == Constants.RelationshipKind.HasEvent);
        
        var owningRelationships = await dbContext.Relationships.Where(r => r.OwnerId == andEvent.Id).ToListAsync();
        owningRelationships.Should().HaveCount(2);
        owningRelationships.Should().Contain(r => 
            r.RelatedId == event1.Id && 
            r.Kind == Constants.RelationshipKind.HasSubEvent);
        owningRelationships.Should().Contain(r => 
            r.RelatedId == event2.Id && 
            r.Kind == Constants.RelationshipKind.HasSubEvent);
    }

}
