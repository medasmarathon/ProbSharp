using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddOrEvent;

public class CreateOwningRelationshipsForAddOrEventRequest(AddOrEventRequest entity) : CreateOwningRelationshipsFor<AddOrEventRequest>(entity)
{
}

public class CreateOrEventOwningRelationshipsHandler : IRequestHandler<CreateOwningRelationshipsForAddOrEventRequest, List<Relationship>>
{
    public ValueTask<List<Relationship>> Handle(CreateOwningRelationshipsForAddOrEventRequest request, CancellationToken token = default)
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
