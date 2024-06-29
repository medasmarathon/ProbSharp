namespace ProbSharp.Persistence
{
    public class Relationship
    {
        public long Id { get; set; }
        public string Kind { get; set; }
        public long OwnerId { get; set; }
        public Node Owner { get; set; }
        public long RelatedId { get; set; }
        public Node Related { get; set; }
        public string Attributes { get; set; }
    }
}
