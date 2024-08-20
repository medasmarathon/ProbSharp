using App.Entities;
using App.Operations.Interfaces;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAndEvent;

public class AddAndEventHandler(
        ProbSharpContext context,
        INodeFactory<AddAndEventRequest> nodeFactory, 
        IRelationshipFactory<AddAndEventRequest> relationshipFactory
    ) : IRequestHandler<AddAndEventRequest, AndEvent>
{
    public async ValueTask<AndEvent> Handle(AddAndEventRequest request, CancellationToken token = default)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var andNodes = nodeFactory.CreateNodes(request);
            context.Nodes.AddRange(andNodes);
            await context.SaveChangesAsync(token);

            var relatedRelationships = relationshipFactory.CreateRelatedRelationships(request);
            relatedRelationships.ForEach(r => r.RelatedId = andNodes[0].Id);

            var owningRelationships = relationshipFactory.CreateOwningRelationships(request);
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
