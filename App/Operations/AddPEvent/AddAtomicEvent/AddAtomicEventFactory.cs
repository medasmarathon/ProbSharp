using System.Text.Json;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class AddAtomicEventFactory : INodeFactory<AddAtomicEventRequest>, IRelationshipFactory<AddAtomicEventRequest>
{
    public List<Node> CreateNodes(AddAtomicEventRequest request)
    {
        var eventNode = new Node
        {
            Label = Constants.NodeLabel.Event,
            Attributes = JsonSerializer.Serialize(new
            {
                request.Name,
                request.EventType,
                request.Probability
            })
        };
        return [eventNode];
    }

    public List<Relationship> CreateRelationships(AddAtomicEventRequest request)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.SampleSpaceId,
            Attributes = JsonSerializer.Serialize(new {})
        };
        var relationshipOfOutcome = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.Outcome.Id,
            Attributes = JsonSerializer.Serialize(new { })
        };
        return [relationshipOfSampleSpace];
    }

}
