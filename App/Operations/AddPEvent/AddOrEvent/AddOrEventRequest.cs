using App.Entities;

namespace App.Operations.AddPEvent.AddOrEvent;

public class AddOrEventRequest
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
    public PEventType EventType = PEventType.Or;
    public decimal? Probability { get; set; }
    public List<PEvent> SubEvents { get; set; }
}
