using App.Entities;
using App.Operations.Common;
using App.Operations.Interfaces;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddSampleSpace;

public class AddSampleSpaceHandler(ProbSharpContext context, IMediator mediator) : IRequestHandler<AddSampleSpaceRequest, SampleSpace>
{
    public async ValueTask<SampleSpace> Handle(AddSampleSpaceRequest request, CancellationToken token = default)
    {
        await using var transaction = context.Database.BeginTransaction();
        try
        {
            var ssNodes = await mediator.Send(new CreateNodesForAddSampleSpaceRequest(request), token);
            context.Nodes.AddRange(ssNodes);
            await context.SaveChangesAsync(token);
            transaction.Commit();

            return new()
            {
                Id = ssNodes[0].Id,
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
