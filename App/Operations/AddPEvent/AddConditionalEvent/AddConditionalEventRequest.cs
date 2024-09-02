using App.Entities;
using Mediator;

namespace App.Operations.AddPEvent.AddConditionalEvent;

public class AddConditionalEventRequest : IRequest<ConditionalEvent>
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
    public PEventType EventType = PEventType.Conditional;
    public decimal? Probability { get; set; }
    public PEvent SubjectEvent { get; set; }
    public PEvent ConditionEvent { get; set; }

}
