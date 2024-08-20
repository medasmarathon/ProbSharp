using App.Entities;
using App.Operations.AddOutcome;
using App.Operations.AddPEvent.AddAndEvent;
using App.Operations.AddPEvent.AddAtomicEvent;
using App.Operations.AddPEvent.AddOrEvent;
using FluentValidation;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent;

public class AddPEventHandler(ProbSharpContext context, IValidator<AddPEventRequest> validator, IMediator appMediator) : IRequestHandler<AddPEventRequest, PEvent>
{
    public async ValueTask<PEvent> Handle(AddPEventRequest request, CancellationToken token = default)
    {
        validator.ValidateAndThrow(request);
        if (request.EventType == PEventType.Atomic)
        {
            return await AddAtomicEvent(request);
        }

        if (request.EventType == PEventType.And)
        {
            return await AddAndEvent(request);
        }
        if (request.EventType == PEventType.Or)
        {
            return await AddOrEvent(request);
        }
        return default;
    }

    private async Task<AtomicEvent> AddAtomicEvent(AddPEventRequest request)
    {
        if (request.Outcome!.Id == 0)
        {
            request.Outcome = await appMediator.Send(new AddOutcomeRequest
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
        return await appMediator.Send(atomicRequest);
    }

    private async Task<AndEvent> AddAndEvent(AddPEventRequest request)
    {
        if (request.SubEvents is null)
            throw new ApplicationException("No Sub event linked to the requested insert And Event");

        var nonExistSubEvents = request.SubEvents?.Where(se => se.Id == 0).ToList();
        if (nonExistSubEvents is null || nonExistSubEvents.Count > 1)
            throw new ApplicationException("Some Sub Events don't exist");
        var andRequest = new AddAndEventRequest()
        {
            Name = request.Name,
            Probability = request.Probability,
            SampleSpaceId = request.SampleSpaceId,
            SubEvents = request.SubEvents
        };
        return await appMediator.Send(andRequest);
    }

    private async Task<OrEvent> AddOrEvent(AddPEventRequest request)
    {
        if (request.SubEvents is null)
            throw new ApplicationException("No Sub event linked to the requested insert And Event");

        var nonExistSubEvents = request.SubEvents?.Where(se => se.Id == 0).ToList();
        if (nonExistSubEvents is null || nonExistSubEvents.Count > 1)
            throw new ApplicationException("Some Sub Events don't exist");
        var orRequest = new AddOrEventRequest()
        {
            Name = request.Name,
            Probability = request.Probability,
            SampleSpaceId = request.SampleSpaceId,
            SubEvents = request.SubEvents
        };
        return await appMediator.Send(orRequest);
    }
}
