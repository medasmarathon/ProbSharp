using App.Entities;
using Mediator;

namespace App.Operations.AddPEvent.AddAndEvent;

public class AddAndEventRequest : IRequest<AndEvent>
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
    public PEventType EventType = PEventType.And;
    public decimal? Probability { get; set; }
    public List<PEvent> SubEvents { get; set; }
}
