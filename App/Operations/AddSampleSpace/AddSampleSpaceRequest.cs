using App.Entities;
using Mediator;

namespace App.Operations.AddSampleSpace;

public class AddSampleSpaceRequest : IRequest<SampleSpace>
{
    public string Name { get; set; } = string.Empty;
}
