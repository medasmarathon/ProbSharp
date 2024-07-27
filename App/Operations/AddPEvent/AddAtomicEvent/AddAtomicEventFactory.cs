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

    public List<Relationship> CreateOwningRelationships(AddAtomicEventRequest request)
    {
        throw new NotImplementedException();
    }

    public List<Relationship> CreateRelatedRelationships(AddAtomicEventRequest request)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.SampleSpaceId
        };
        var relationshipOfOutcome = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.Outcome.Id
        };
        return [relationshipOfSampleSpace, relationshipOfOutcome];
    }

}
