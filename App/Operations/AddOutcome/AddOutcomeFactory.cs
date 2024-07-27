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

    public List<Relationship> CreateOwningRelationships(AddOutcomeRequest request)
    {
        throw new NotImplementedException();
    }

    public List<Relationship> CreateRelatedRelationships(AddOutcomeRequest request)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasOutcome,
            OwnerId = request.SampleSpaceId
        };
        return [ relationshipOfSampleSpace ];
    }

}
