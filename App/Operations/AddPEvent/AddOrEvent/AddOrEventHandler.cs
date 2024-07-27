using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddOrEvent;

public class AddOrEventHandler(
        ProbSharpContext context,
        INodeFactory<AddOrEventRequest> nodeFactory, 
        IRelationshipFactory<AddOrEventRequest> relationshipFactory
    ) : IRequestHandler<AddOrEventRequest, OrEvent>
{
    public async Task<OrEvent> Handle(AddOrEventRequest request)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var orNodes = nodeFactory.CreateNodes(request);
            context.Nodes.AddRange(orNodes);
            await context.SaveChangesAsync();

            var relatedRelationships = relationshipFactory.CreateRelatedRelationships(request);
            relatedRelationships.ForEach(r =>
            {
                r.RelatedId = orNodes.First().Id;
            });
            
            var owningRelationships = relationshipFactory.CreateOwningRelationships(request);
            owningRelationships.ForEach(r =>
            {
                r.OwnerId = orNodes.First().Id;
            });
            context.Relationships.AddRange([ ..relatedRelationships, ..owningRelationships]);
            await context.SaveChangesAsync();
            transaction.Commit();

            return new()
            {
                Id = orNodes.First().Id,
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
