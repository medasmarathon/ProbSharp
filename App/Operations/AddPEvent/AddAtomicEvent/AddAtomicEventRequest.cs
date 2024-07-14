using App.Entities;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class AddAtomicEventRequest
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
    public PEventType EventType = PEventType.Atomic;
    public decimal? Probability { get; set; }
    public Outcome Outcome { get; set; }
}
