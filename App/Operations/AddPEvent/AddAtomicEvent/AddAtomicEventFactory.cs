using System.Text.Json;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class AddAtomicEventFactory : INodeFactory<AddAtomicEventRequest>, IRelationshipFactory<AddAtomicEventRequest>
{
    public Node CreateNode(AddAtomicEventRequest request)
    {
        var eventNode = new Node
        {
            Label = Constants.NodeLabel.Outcome,
            Attributes = JsonSerializer.Serialize(new
            {
                request.Name,
            })
        };
        return eventNode;
    }

    public Relationship CreateRelationship(AddAtomicEventRequest request)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.SampleSpaceId,
            Attributes = JsonSerializer.Serialize(new {})
        };
        return relationshipOfSampleSpace;
    }

}
