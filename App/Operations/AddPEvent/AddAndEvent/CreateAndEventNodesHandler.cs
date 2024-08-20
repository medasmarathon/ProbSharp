using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAndEvent;

public class CreateNodesForAddAndEventRequest(AddAndEventRequest entity) : CreateNodesFor<AddAndEventRequest>(entity)
{
}

public class CreateAndNodesHandler : IRequestHandler<CreateNodesForAddAndEventRequest, List<Node>>
{
    public ValueTask<List<Node>> Handle(CreateNodesForAddAndEventRequest request, CancellationToken token = default)
    {
        var eventNode = new Node
        {
            Label = Constants.NodeLabel.Event,
            Attributes = JsonSerializer.Serialize(new
            {
                request.Entity.Name,
                request.Entity.EventType,
                request.Entity.Probability
            })
        };
        return new([eventNode]);
    }

}
