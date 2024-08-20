using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAndEvent;

public class CreateOwningRelationshipsForAddAndEventRequest(AddAndEventRequest entity) : CreateOwningRelationshipsFor<AddAndEventRequest>(entity)
{
}

public class CreateAndEventOwningRelationshipsHandler : IRequestHandler<CreateOwningRelationshipsForAddAndEventRequest, List<Relationship>>
{
    public ValueTask<List<Relationship>> Handle(CreateOwningRelationshipsForAddAndEventRequest request, CancellationToken token = default)
    {
        var relationshipsToSubEvents = request.Entity.SubEvents.Select(se =>
        {
            return new Relationship
            {
                Kind = Constants.RelationshipKind.HasSubEvent,
                RelatedId = se.Id
            };
        });
        return new(relationshipsToSubEvents.ToList());
    }

}
