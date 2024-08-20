using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAndEvent;

public class CreateRelatedRelationshipsForAddAndEventRequest(AddAndEventRequest entity) : CreateRelatedRelationshipsFor<AddAndEventRequest>(entity)
{
}

public class CreateAndEventRelatedRelationshipsHandler : IRequestHandler<CreateRelatedRelationshipsForAddAndEventRequest, List<Relationship>>
{
    public ValueTask<List<Relationship>> Handle(CreateRelatedRelationshipsForAddAndEventRequest request, CancellationToken token = default)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.Entity.SampleSpaceId
        };
        return new([relationshipOfSampleSpace]);
    }

}
