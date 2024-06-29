using System.Text.Json;
using App.Operations.Requests;
using ProbSharp.Persistence;

namespace App.Operations.Factories;

public class RelationshipFactory
{
    public Relationship From(AddOutcomeRequest source)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasOutcome,
            OwnerId = source.SampleSpaceId,
            Attributes = JsonSerializer.Serialize(new {})
        };
        return relationshipOfSampleSpace;
    }
}
