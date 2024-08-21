using App.Entities;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class AddAtomicEventHandler(
        ProbSharpContext context,
        IMediator mediator
    ) : IRequestHandler<AddAtomicEventRequest, AtomicEvent>
{
    public async ValueTask<AtomicEvent> Handle(AddAtomicEventRequest request, CancellationToken token = default)
    {
        await using var transaction = context.Database.BeginTransaction();
        try
        {
            var atomicNodes = await mediator.Send(new CreateNodesForAddAtomicEventRequest(request), token);
            var relationships = await mediator.Send(new CreateRelatedRelationshipsForAddAtomicEventRequest(request), token);
            context.Nodes.AddRange(atomicNodes);
            await context.SaveChangesAsync(token);

            relationships.ForEach(r => r.RelatedId = atomicNodes[0].Id);
            context.Relationships.AddRange(relationships);
            await context.SaveChangesAsync(token);
            transaction.Commit();

            return new()
            {
                Id = atomicNodes[0].Id,
                Name = request.Name,
                Probability = request.Probability,
                Outcome = new()
                {
                    Id = relationships[0].OwnerId,
                }
            };
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw new ApplicationException("Error inserting Atomic Event", ex);
        }
    }
}
