using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddSampleSpace;

public class AddSampleSpaceHandler(ProbSharpContext context, INodeFactory<AddSampleSpaceRequest> nodeFactory) : IRequestHandler<AddSampleSpaceRequest, SampleSpace>
{
    public async Task<SampleSpace> Handle(AddSampleSpaceRequest request)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var ssNodes = nodeFactory.CreateNodes(request);
            context.Nodes.AddRange(ssNodes);
            await context.SaveChangesAsync();
            transaction.Commit();

            return new()
            {
                Id = ssNodes.First().Id,
                Name = request.Name,
            };
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw new ApplicationException("Error inserting Sample Space", ex);
        }
    }
}
