using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class CreateRelatedRelationshipsForAddAtomicEventRequest(AddAtomicEventRequest entity) : CreateRelatedRelationshipsFor<AddAtomicEventRequest>(entity)
{
}

public class CreateAtomicRelationshipsHandler : IRequestHandler<CreateRelatedRelationshipsForAddAtomicEventRequest, List<Relationship>>
{
    public ValueTask<List<Relationship>> Handle(CreateRelatedRelationshipsForAddAtomicEventRequest request, CancellationToken token = default)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.Entity.SampleSpaceId
        };
        var relationshipOfOutcome = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.Entity.Outcome.Id
        };
        return new([relationshipOfSampleSpace, relationshipOfOutcome]);
    }

}
