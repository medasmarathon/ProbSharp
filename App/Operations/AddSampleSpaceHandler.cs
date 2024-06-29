using App.Entities;
using App.Operations.Interfaces;
using App.Operations.Requests;
using ProbSharp.Persistence;

namespace App.Operations;

public class AddSampleSpaceHandler : IRequestHandler<AddSampleSpaceRequest, SampleSpace>
{
    private readonly ProbSharpContext _context;
    public AddSampleSpaceHandler(ProbSharpContext context)
    {
        _context = context;
    }
    public async Task<SampleSpace> Handle(AddSampleSpaceRequest request)
    {
        throw new NotImplementedException();
    }
}
