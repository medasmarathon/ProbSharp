using App.Entities;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddOrEvent;

public class AddOrEventHandler(
        ProbSharpContext context,
        IMediator mediator
    ) : IRequestHandler<AddOrEventRequest, OrEvent>
{
    public async ValueTask<OrEvent> Handle(AddOrEventRequest request, CancellationToken token = default)
    {
        await using var transaction = context.Database.BeginTransaction();
        try
        {
            var orNodes = await mediator.Send(new CreateNodesForAddOrEventRequest(request), token);
            context.Nodes.AddRange(orNodes);
            await context.SaveChangesAsync(token);

            var relatedRelationships = await mediator.Send(new CreateRelatedRelationshipsForAddOrEventRequest(request), token);
            relatedRelationships.ForEach(r => r.RelatedId = orNodes[0].Id);

            var owningRelationships = await mediator.Send(new CreateOwningRelationshipsForAddOrEventRequest(request), token);
            owningRelationships.ForEach(r => r.OwnerId = orNodes[0].Id);
            context.Relationships.AddRange([ ..relatedRelationships, ..owningRelationships]);
            await context.SaveChangesAsync(token);
            transaction.Commit();

            return new()
            {
                Id = orNodes[0].Id,
                Name = request.Name,
                Probability = request.Probability,
            };
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw new ApplicationException("Error inserting And Event", ex);
        }
    }
}
