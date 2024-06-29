using App.Entities;
using App.Operations.Factories;
using App.Operations.Interfaces;
using App.Operations.Requests;
using ProbSharp.Persistence;

namespace App.Operations;

public class AddOutcomeHandler : IRequestHandler<AddOutcomeRequest, Outcome>
{
    private readonly ProbSharpContext _context;
    private readonly NodeFactory _nodeFactory;
    private readonly RelationshipFactory _relationshipFactory;
    public AddOutcomeHandler(ProbSharpContext context, NodeFactory nodeFactory, RelationshipFactory relationshipFactory)
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
            var outcomeNode = _nodeFactory.From(request);
            var relationship = _relationshipFactory.From(request);
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
