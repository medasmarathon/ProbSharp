using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddOrEvent;

public class CreateRelatedRelationshipsForAddOrEventRequest(AddOrEventRequest entity) : CreateRelatedRelationshipsFor<AddOrEventRequest>(entity)
{
}

public class CreateOrEventRelatedRelationshipsHandler : IRequestHandler<CreateRelatedRelationshipsForAddOrEventRequest, List<Relationship>>
{
    public ValueTask<List<Relationship>> Handle(CreateRelatedRelationshipsForAddOrEventRequest request, CancellationToken token = default)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.Entity.SampleSpaceId
        };
        return new([relationshipOfSampleSpace]);
    }

}
