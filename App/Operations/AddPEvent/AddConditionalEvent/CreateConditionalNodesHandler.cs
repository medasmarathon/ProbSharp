using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddConditionalEvent;

public class CreateNodesForAddConditionalEventRequest(AddConditionalEventRequest entity) : CreateNodesFor<AddConditionalEventRequest>(entity)
{
}

public class CreateConditionalNodesHandler : IRequestHandler<CreateNodesForAddConditionalEventRequest, List<Node>>
{
    public ValueTask<List<Node>> Handle(CreateNodesForAddConditionalEventRequest request, CancellationToken token = default)
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
