using App.Entities;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddOutcome;

public class AddOutcomeHandler(ProbSharpContext context, IMediator mediator) : IRequestHandler<AddOutcomeRequest, Outcome>
{
    public async ValueTask<Outcome> Handle(AddOutcomeRequest request, CancellationToken token = default)
    {
        await using var transaction = context.Database.BeginTransaction();
        try
        {
            var outcomeNodes = await mediator.Send(new CreateNodesForAddOutcomeRequest(request), token);
            var relationships = await mediator.Send(new CreateRelatedRelationshipsForAddOutcomeRequest(request), token);
            context.Nodes.AddRange(outcomeNodes);
            await context.SaveChangesAsync(token);

            relationships[0].RelatedId = outcomeNodes[0].Id;
            context.Relationships.AddRange(relationships);
            await context.SaveChangesAsync(token);
            transaction.Commit();

            return new()
            {
                Id = outcomeNodes[0].Id,
                Name = request.Name,
                SampleSpace = new()
                {
                    Id = relationships[0].OwnerId,
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
