using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddOrEvent;

public class CreateNodesForAddOrEventRequest(AddOrEventRequest entity) : CreateNodesFor<AddOrEventRequest>(entity)
{
}
public class CreateOrNodesHandler : IRequestHandler<CreateNodesForAddOrEventRequest, List<Node>>
{
    public ValueTask<List<Node>> Handle(CreateNodesForAddOrEventRequest request, CancellationToken token = default)
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
