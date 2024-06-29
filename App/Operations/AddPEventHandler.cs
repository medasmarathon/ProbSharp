using App.Entities;
using App.Operations.Interfaces;
using App.Operations.Requests;
using ProbSharp.Persistence;

namespace App.Operations;

public class AddPEventHandler : IRequestHandler<AddPEventRequest, PEvent>
{
    private readonly ProbSharpContext _context;
    public AddPEventHandler(ProbSharpContext context)
    {
        _context = context;
    }
    public async Task<PEvent> Handle(AddPEventRequest request)
    {
        throw new NotImplementedException();
    }
}
