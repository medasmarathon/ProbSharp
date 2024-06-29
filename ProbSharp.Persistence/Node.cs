namespace ProbSharp.Persistence
{
    public class Node
    {
        public long Id { get; set; }
        public string Label { get; set; }
        public string? Attributes { get; set; }
        public ICollection<Relationship>? Has { get; set; }
        public ICollection<Relationship>? BelongsTo { get; set; }
    }
}
