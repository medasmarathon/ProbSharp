using App.Entities;
using App.Operations.AddOutcome;
using App.Operations.AddPEvent.AddAtomicEvent;
using App.Operations.Interfaces;
using FluentValidation;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent;

public class AddPEventHandler(ProbSharpContext context, IValidator<AddPEventRequest> validator, Operator appOperator) : IRequestHandler<AddPEventRequest, PEvent>
{
    public async Task<PEvent> Handle(AddPEventRequest request)
    {
        validator.ValidateAndThrow(request);
        if (request.EventType == PEventType.Atomic)
        {
            return await AddAtomicEvent(request);
        }
        return default;
    }

    private async Task<AtomicEvent> AddAtomicEvent(AddPEventRequest request)
    {
        if (request.Outcome!.Id == 0)
        {
            request.Outcome = await appOperator.Send(new AddOutcomeRequest
            {
                Name = request.Outcome.Name,
                SampleSpaceId = request.SampleSpaceId
            });
        }
        var atomicRequest = new AddAtomicEventRequest()
        {
            Name = request.Name,
            Probability = request.Probability,
            SampleSpaceId = request.SampleSpaceId,
            Outcome = request.Outcome!
        };
        return await appOperator.Send(atomicRequest);
    }
}
