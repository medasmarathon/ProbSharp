using App.Entities;

namespace App.Operations.AddPEvent;

public class AddPEventRequest
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
    public PEventType EventType { get; set; }
    public decimal? Probability { get; set; }
    public Outcome? Outcome { get; set; }
    public List<PEvent>? SubEvents { get; set; }
    public PEvent? SubjectEvent { get; set; }
    public PEvent? ConditionEvent { get; set; }
}
