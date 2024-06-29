namespace App.Entities;

public class PEvent
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PEventType Type { get; set; }
    public decimal? Probability { get; set; }
}

public enum PEventType
{
    Conditional,
    And,
    Or,
    Atomic
}

public class AtomicEvent : PEvent
{
    public Outcome? Outcome { get; set; }
    public new PEventType Type { get => PEventType.Atomic; }
}

public class AndEvent : PEvent
{
    public new PEventType Type { get => PEventType.And; }
    public List<PEvent> SubEvents { get; set; } = new();
}

public class OrEvent : PEvent
{
    public new PEventType Type { get => PEventType.Or; }
    public List<PEvent> SubEvents { get; set; } = new();
}

public class ConditionalEvent : PEvent
{
    public new PEventType Type { get => PEventType.Conditional; }
    public PEvent? SubjectEvent { get; set; }
    public PEvent? ConditionEvent { get; set; }
}