using System.Text.Json;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddOutcome;

public class AddOutcomeFactory : INodeFactory<AddOutcomeRequest>, IRelationshipFactory<AddOutcomeRequest>
{
    public List<Node> CreateNodes(AddOutcomeRequest request)
    {
        var outcomeNode = new Node
        {
            Label = Constants.NodeLabel.Outcome,
            Attributes = JsonSerializer.Serialize(new
            {
                request.Name,
            })
        };
        return [ outcomeNode ];
    }

    public List<Relationship> CreateRelationships(AddOutcomeRequest request)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasOutcome,
            OwnerId = request.SampleSpaceId,
            Attributes = JsonSerializer.Serialize(new {})
        };
        return [ relationshipOfSampleSpace ];
    }

}
