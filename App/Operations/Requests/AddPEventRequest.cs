namespace App.Operations.Requests;

public class AddPEventRequest
{
    public string Name { get; set; } = string.Empty;
    public long SampleSpaceId { get; set; }
}
