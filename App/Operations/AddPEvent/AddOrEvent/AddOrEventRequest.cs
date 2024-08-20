using App.Entities;
using Mediator;

namespace App.Operations.AddPEvent.AddOrEvent;

public class AddOrEventRequest : IRequest<OrEvent>
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
    public PEventType EventType = PEventType.Or;
    public decimal? Probability { get; set; }
    public List<PEvent> SubEvents { get; set; } = [];
}
