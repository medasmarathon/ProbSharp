using App.Entities;
using App.Operations.Interfaces;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddOrEvent;

public class AddOrEventHandler(
        ProbSharpContext context,
        INodeFactory<AddOrEventRequest> nodeFactory, 
        IRelationshipFactory<AddOrEventRequest> relationshipFactory
    ) : IRequestHandler<AddOrEventRequest, OrEvent>
{
    public async ValueTask<OrEvent> Handle(AddOrEventRequest request, CancellationToken token = default)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var orNodes = nodeFactory.CreateNodes(request);
            context.Nodes.AddRange(orNodes);
            await context.SaveChangesAsync(token);

            var relatedRelationships = relationshipFactory.CreateRelatedRelationships(request);
            relatedRelationships.ForEach(r => r.RelatedId = orNodes[0].Id);

            var owningRelationships = relationshipFactory.CreateOwningRelationships(request);
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
