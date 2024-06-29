using System.Text.Json;
using App.Operations.Requests;
using ProbSharp.Persistence;

namespace App.Operations.Factories;

public class NodeFactory
{
    public Node From(AddOutcomeRequest request)
    {
        var outcomeNode = new Node
        {
            Label = Constants.NodeLabel.Outcome,
            Attributes = JsonSerializer.Serialize(new
            {
                request.Name,
            })
        };
        return outcomeNode;
    }

    public Node From(AddSampleSpaceRequest request)
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
