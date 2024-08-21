using App.Entities;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAndEvent;

public class AddAndEventHandler(
        ProbSharpContext context,
        IMediator mediator
    ) : IRequestHandler<AddAndEventRequest, AndEvent>
{
    public async ValueTask<AndEvent> Handle(AddAndEventRequest request, CancellationToken token = default)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var andNodes = await mediator.Send(new CreateNodesForAddAndEventRequest(request), token);
            context.Nodes.AddRange(andNodes);
            await context.SaveChangesAsync(token);

            var relatedRelationships = await mediator.Send(new CreateRelatedRelationshipsForAddAndEventRequest(request), token);
            relatedRelationships.ForEach(r => r.RelatedId = andNodes[0].Id);

            var owningRelationships = await mediator.Send(new CreateOwningRelationshipsForAddAndEventRequest(request), token);
            owningRelationships.ForEach(r => r.OwnerId = andNodes[0].Id);
            context.Relationships.AddRange([ ..relatedRelationships, ..owningRelationships]);
            await context.SaveChangesAsync(token);
            transaction.Commit();

            return new()
            {
                Id = andNodes[0].Id,
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
