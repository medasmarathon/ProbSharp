using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddOutcome;

public class CreateNodesForAddOutcomeRequest(AddOutcomeRequest entity) : CreateNodesFor<AddOutcomeRequest>(entity)
{
}

public class CreateOutcomeNodesHandler : IRequestHandler<CreateNodesForAddOutcomeRequest, List<Node>>
{
    public ValueTask<List<Node>> Handle(CreateNodesForAddOutcomeRequest request, CancellationToken token = default)
    {
        var outcomeNode = new Node
        {
            Label = Constants.NodeLabel.Outcome,
            Attributes = JsonSerializer.Serialize(new
            {
                request.Entity.Name,
            })
        };
        return new([outcomeNode]);
    }

}
