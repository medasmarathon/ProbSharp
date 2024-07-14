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
            var outcomeNode = nodeFactory.CreateNode(request);
            var relationship = relationshipFactory.CreateRelationship(request);
            context.Nodes.Add(outcomeNode);
            await context.SaveChangesAsync();

            relationship.RelatedId = outcomeNode.Id;
            context.Relationships.Add(relationship);
            await context.SaveChangesAsync();
            transaction.Commit();

            return new()
            {
                Id = outcomeNode.Id,
                Name = request.Name,
                SampleSpace = new()
                {
                    Id = relationship.OwnerId,
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
