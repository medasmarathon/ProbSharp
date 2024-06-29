namespace App.Entities;

public class Outcome
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SampleSpace? SampleSpace { get; set; }
}
