using App.Entities;
using App.Operations.Interfaces;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class AddAtomicEventHandler(
        ProbSharpContext context,
        INodeFactory<AddAtomicEventRequest> nodeFactory, 
        IRelationshipFactory<AddAtomicEventRequest> relationshipFactory
    ) : IRequestHandler<AddAtomicEventRequest, AtomicEvent>
{
    public async ValueTask<AtomicEvent> Handle(AddAtomicEventRequest request, CancellationToken token = default)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var atomicNodes = nodeFactory.CreateNodes(request);
            var relationships = relationshipFactory.CreateRelatedRelationships(request);
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
