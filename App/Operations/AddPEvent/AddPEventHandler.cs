using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent;

public class AddPEventHandler(ProbSharpContext context) : IRequestHandler<AddPEventRequest, PEvent>
{
    public async Task<PEvent> Handle(AddPEventRequest request)
    {
        throw new NotImplementedException();
    }
}
