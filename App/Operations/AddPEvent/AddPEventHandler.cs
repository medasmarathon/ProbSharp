using App.Entities;
using App.Operations.Interfaces;
using FluentValidation;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent;

public class AddPEventHandler(ProbSharpContext context, IValidator<AddPEventRequest> validator) : IRequestHandler<AddPEventRequest, PEvent>
{
    public async Task<PEvent> Handle(AddPEventRequest request)
    {
        validator.ValidateAndThrow(request);
        return default;
    }
}
