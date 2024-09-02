using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddConditionalEvent;

public class CreateRelatedRelationshipsForAddConditionalEventRequest(AddConditionalEventRequest entity) : CreateRelatedRelationshipsFor<AddConditionalEventRequest>(entity)
{
}

public class CreateConditionalRelationshipsHandler : IRequestHandler<CreateRelatedRelationshipsForAddConditionalEventRequest, List<Relationship>>
{
    public ValueTask<List<Relationship>> Handle(CreateRelatedRelationshipsForAddConditionalEventRequest request, CancellationToken token = default)
    {
        var relationshipOfSampleSpace = new Relationship
        {
            Kind = Constants.RelationshipKind.HasEvent,
            OwnerId = request.Entity.SampleSpaceId
        };
        return new([relationshipOfSampleSpace]);
    }

}
