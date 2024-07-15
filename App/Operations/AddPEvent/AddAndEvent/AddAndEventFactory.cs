using System.Text.Json;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAndEvent;

public class AddAndEventFactory : INodeFactory<AddAndEventRequest>, IRelationshipFactory<AddAndEventRequest>
{
    public List<Node> CreateNodes(AddAndEventRequest request)
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

    public List<Relationship> CreateRelationships(AddAndEventRequest request)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.SampleSpaceId,
            Attributes = JsonSerializer.Serialize(new {})
        };
        return [relationshipOfSampleSpace];
    }

}
