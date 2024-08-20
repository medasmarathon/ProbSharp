using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddOutcome;

public class CreateRelatedRelationshipsForAddOutcomeRequest(AddOutcomeRequest entity) : CreateRelatedRelationshipsFor<AddOutcomeRequest>(entity)
{
}

public class CreateOutcomeRelatedRelationshipsHandler : IRequestHandler<CreateRelatedRelationshipsForAddOutcomeRequest, List<Relationship>>
{
    public ValueTask<List<Relationship>> Handle(CreateRelatedRelationshipsForAddOutcomeRequest request, CancellationToken token = default)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasOutcome,
            OwnerId = request.Entity.SampleSpaceId
        };
        return new([relationshipOfSampleSpace]);
    }

}
