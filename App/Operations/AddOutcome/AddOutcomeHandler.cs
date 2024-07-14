using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddOutcome;

public class AddOutcomeHandler(ProbSharpContext context, INodeFactory<AddOutcomeRequest> nodeFactory, IRelationshipFactory<AddOutcomeRequest> relationshipFactory) : IRequestHandler<AddOutcomeRequest, Outcome>
{
    public async Task<Outcome> Handle(AddOutcomeRequest request)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var outcomeNodes = nodeFactory.CreateNodes(request);
            var relationships = relationshipFactory.CreateRelationships(request);
            context.Nodes.AddRange(outcomeNodes);
            await context.SaveChangesAsync();

            relationships.First().RelatedId = outcomeNodes.First().Id;
            context.Relationships.AddRange(relationships);
            await context.SaveChangesAsync();
            transaction.Commit();

            return new()
            {
                Id = outcomeNodes.First().Id,
                Name = request.Name,
                SampleSpace = new()
                {
                    Id = relationships.First().OwnerId,
                }
            };
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw new ApplicationException("Error inserting Outcome", ex);
        }
        
    }
}
