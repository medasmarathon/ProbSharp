using App.Entities;
using App.Operations.Factories;
using App.Operations.Interfaces;
using App.Operations.Requests;
using ProbSharp.Persistence;

namespace App.Operations;

public class AddSampleSpaceHandler : IRequestHandler<AddSampleSpaceRequest, SampleSpace>
{
    private readonly ProbSharpContext _context;
    private readonly NodeFactory _nodeFactory;
    private readonly RelationshipFactory _relationshipFactory;
    public AddSampleSpaceHandler(ProbSharpContext context, NodeFactory nodeFactory, RelationshipFactory relationshipFactory)
    {
        _context = context;
        _nodeFactory = nodeFactory;
        _relationshipFactory = relationshipFactory;
    }
    public async Task<SampleSpace> Handle(AddSampleSpaceRequest request)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var ssNode = _nodeFactory.From(request);
            _context.Nodes.Add(ssNode);
            await _context.SaveChangesAsync();
            transaction.Commit();

            return new()
            {
                Id = ssNode.Id,
                Name = request.Name,
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error inserting Sample Space", ex);
        }
    }
}
