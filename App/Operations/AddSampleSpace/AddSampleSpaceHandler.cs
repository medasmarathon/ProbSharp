using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddSampleSpace;

public class AddSampleSpaceHandler : IRequestHandler<AddSampleSpaceRequest, SampleSpace>
{
    private readonly ProbSharpContext _context;
    private readonly INodeFactory<AddSampleSpaceRequest> _requestFactory;
    public AddSampleSpaceHandler(ProbSharpContext context, INodeFactory<AddSampleSpaceRequest> requestFactory)
    {
        _context = context;
        _requestFactory = requestFactory;
    }
    public async Task<SampleSpace> Handle(AddSampleSpaceRequest request)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var ssNode = _requestFactory.CreateNode(request);
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
