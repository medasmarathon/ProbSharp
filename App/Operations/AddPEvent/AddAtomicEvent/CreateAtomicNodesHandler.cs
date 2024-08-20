using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class CreateNodesForAddAtomicEventRequest(AddAtomicEventRequest entity) : CreateNodesFor<AddAtomicEventRequest>(entity)
{
}

public class CreateAtomicNodesHandler : IRequestHandler<CreateNodesForAddAtomicEventRequest, List<Node>>
{
    public ValueTask<List<Node>> Handle(CreateNodesForAddAtomicEventRequest request, CancellationToken token = default)
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
