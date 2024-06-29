using App.Entities;
using App.Operations.Interfaces;
using App.Operations.Requests;
using ProbSharp.Persistence;

namespace App.Operations;

public class AddOutcomeHandler : IRequestHandler<AddOutcomeRequest, Outcome>
{
    private readonly ProbSharpContext _context;
    public AddOutcomeHandler(ProbSharpContext context)
    {
        _context = context;
    }
    public async Task<Outcome> Handle(AddOutcomeRequest request)
    {
        throw new NotImplementedException();
    }
}
