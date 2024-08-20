using App.Entities;
using Mediator;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class AddAtomicEventRequest : IRequest<AtomicEvent>
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
    public PEventType EventType = PEventType.Atomic;
    public decimal? Probability { get; set; }
    public Outcome Outcome { get; set; }
}
