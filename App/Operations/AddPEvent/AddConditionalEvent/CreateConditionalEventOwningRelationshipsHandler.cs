using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddConditionalEvent;

public class CreateOwningRelationshipsForAddConditionalEventRequest(AddConditionalEventRequest entity) : CreateOwningRelationshipsFor<AddConditionalEventRequest>(entity)
{
}

public class CreateConditionalEventOwningRelationshipsHandler : IRequestHandler<CreateOwningRelationshipsForAddConditionalEventRequest, List<Relationship>>
{
    public ValueTask<List<Relationship>> Handle(CreateOwningRelationshipsForAddConditionalEventRequest request, CancellationToken token = default)
    {
        List<Relationship> relationships = [
            new Relationship
            {
                Kind = Constants.RelationshipKind.HasSubjectEvent,
                RelatedId = request.Entity.SubjectEvent.Id
            },
            new Relationship
            {
                Kind = Constants.RelationshipKind.HasConditionEvent,
                RelatedId = request.Entity.ConditionEvent.Id
            }
        ];
        return ValueTask.FromResult(relationships);
    }

}
