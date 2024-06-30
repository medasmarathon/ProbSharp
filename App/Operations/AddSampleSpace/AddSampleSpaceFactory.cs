using System.Text.Json;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddSampleSpace;

public class AddSampleSpaceFactory : INodeFactory<AddSampleSpaceRequest>
{
    public Node CreateNode(AddSampleSpaceRequest request)
    {
        var ssNode = new Node
        {
            Label = Constants.NodeLabel.SampleSpace,
            Attributes = JsonSerializer.Serialize(new
            {
                request.Name,
            })
        };
        return ssNode;
    }

}
