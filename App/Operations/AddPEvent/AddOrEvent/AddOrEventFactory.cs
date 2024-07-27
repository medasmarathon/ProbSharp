using System.Text.Json;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddOrEvent;

public class AddOrEventFactory : INodeFactory<AddOrEventRequest>, IRelationshipFactory<AddOrEventRequest>
{
    public List<Node> CreateNodes(AddOrEventRequest request)
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

    public List<Relationship> CreateOwningRelationships(AddOrEventRequest request)
    {
        var relationshipsToSubEvents = request.SubEvents.Select(se =>
        {
            return new Relationship
            {
                Kind = Constants.RelationshipKind.HasSubEvent,
                RelatedId = se.Id
            };
        });
        return relationshipsToSubEvents.ToList();
    }

    public List<Relationship> CreateRelatedRelationships(AddOrEventRequest request)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.SampleSpaceId
        };
        return [relationshipOfSampleSpace];
    }

}
