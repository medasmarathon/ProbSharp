using App.Entities;
using Mediator;

namespace App.Operations.AddOutcome;

public class AddOutcomeRequest : IRequest<Outcome>
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
}
