using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAndEvent;

public class AddAndEventHandler(
        ProbSharpContext context,
        INodeFactory<AddAndEventRequest> nodeFactory, 
        IRelationshipFactory<AddAndEventRequest> relationshipFactory
    ) : IRequestHandler<AddAndEventRequest, AndEvent>
{
    public async Task<AndEvent> Handle(AddAndEventRequest request)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var andNodes = nodeFactory.CreateNodes(request);
            context.Nodes.AddRange(andNodes);
            await context.SaveChangesAsync();

            var relatedRelationships = relationshipFactory.CreateRelatedRelationships(request);
            relatedRelationships.ForEach(r =>
            {
                r.RelatedId = andNodes.First().Id;
            });
            
            var owningRelationships = relationshipFactory.CreateOwningRelationships(request);
            owningRelationships.ForEach(r =>
            {
                r.OwnerId = andNodes.First().Id;
            });
            context.Relationships.AddRange([ ..relatedRelationships, ..owningRelationships]);
            await context.SaveChangesAsync();
            transaction.Commit();

            return new()
            {
                Id = andNodes.First().Id,
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
