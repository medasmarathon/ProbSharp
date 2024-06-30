using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddOutcome;

public class AddOutcomeHandler : IRequestHandler<AddOutcomeRequest, Outcome>
{
    private readonly ProbSharpContext _context;
    private readonly INodeFactory<AddOutcomeRequest> _nodeFactory;
    private readonly IRelationshipFactory<AddOutcomeRequest> _relationshipFactory;
    public AddOutcomeHandler(ProbSharpContext context, INodeFactory<AddOutcomeRequest> nodeFactory, IRelationshipFactory<AddOutcomeRequest> relationshipFactory)
    {
        _context = context;
        _nodeFactory = nodeFactory;
        _relationshipFactory = relationshipFactory;
    }
    public async Task<Outcome> Handle(AddOutcomeRequest request)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var outcomeNode = _nodeFactory.CreateNode(request);
            var relationship = _relationshipFactory.CreateRelationship(request);
            _context.Nodes.Add(outcomeNode);
            await _context.SaveChangesAsync();

            relationship.RelatedId = outcomeNode.Id;
            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();
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
            throw new ApplicationException("Error inserting Outcome", ex);
        }
        
    }
}
