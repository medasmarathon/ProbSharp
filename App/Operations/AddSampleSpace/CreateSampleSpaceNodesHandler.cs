using System.Text.Json;
using App.Operations.Common;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddSampleSpace;

public class CreateNodesForAddSampleSpaceRequest(AddSampleSpaceRequest entity) : CreateNodesFor<AddSampleSpaceRequest>(entity)
{
}

public class CreateSampleSpaceNodesHandler : IRequestHandler<CreateNodesForAddSampleSpaceRequest, List<Node>>
{
    public ValueTask<List<Node>> Handle(CreateNodesForAddSampleSpaceRequest request, CancellationToken token = default)
    {
        var ssNode = new Node
        {
            Label = Constants.NodeLabel.SampleSpace,
            Attributes = JsonSerializer.Serialize(new
            {
                request.Entity.Name,
            })
        };
        return new([ssNode]);
    }

}
