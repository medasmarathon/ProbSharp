namespace App.Operations;

public class Constants
{
    public static class NodeLabel
    {
        public const string Outcome = "outcome";
        public const string SampleSpace = "sample_space";
        public const string Event = "event";
    }
    
    public static class RelationshipKind
    {
        public const string HasOutcome = "has_outcome";
        public const string HasEvent = "has_event";
        public const string HasSubEvent = "has_sub_event";
        public const string HasSubjectEvent = "has_subject_event";
        public const string HasConditionEvent = "has_condition_event";
    }

}
