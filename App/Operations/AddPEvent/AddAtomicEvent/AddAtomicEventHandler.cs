using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class AddAtomicEventHandler(
        ProbSharpContext context,
        INodeFactory<AddAtomicEventRequest> nodeFactory, 
        IRelationshipFactory<AddAtomicEventRequest> relationshipFactory
    ) : IRequestHandler<AddAtomicEventRequest, AtomicEvent>
{
    public async Task<AtomicEvent> Handle(AddAtomicEventRequest request)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var atomicNodes = nodeFactory.CreateNodes(request);
            var relationships = relationshipFactory.CreateRelatedRelationships(request);
            context.Nodes.AddRange(atomicNodes);
            await context.SaveChangesAsync();

            relationships.ForEach(r =>
            {
                r.RelatedId = atomicNodes.First().Id;
            });
            context.Relationships.AddRange(relationships);
            await context.SaveChangesAsync();
            transaction.Commit();

            return new()
            {
                Id = atomicNodes.First().Id,
                Name = request.Name,
                Probability = request.Probability,
                Outcome = new()
                {
                    Id = relationships.First().OwnerId,
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
