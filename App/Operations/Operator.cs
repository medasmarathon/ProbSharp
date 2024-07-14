using App.Entities;
using App.Operations.AddOutcome;
using App.Operations.AddPEvent;
using App.Operations.AddPEvent.AddAtomicEvent;
using App.Operations.AddSampleSpace;
using App.Operations.Interfaces;

namespace App.Operations;

public class Operator(
        IRequestHandler<AddOutcomeRequest, Outcome> outcomeHandler,
        IRequestHandler<AddSampleSpaceRequest, SampleSpace> sampleSpaceHandler,
        IRequestHandler<AddAtomicEventRequest, AtomicEvent> atomicEventHandler,
        IRequestHandler<AddPEventRequest, PEvent> eventHandler
    ) : 
    IOperator<AddOutcomeRequest, Outcome>,
    IOperator<AddSampleSpaceRequest, SampleSpace>,
    IOperator<AddAtomicEventRequest, AtomicEvent>,
    IOperator<AddPEventRequest, PEvent>
{
    public async Task<Outcome> Send(AddOutcomeRequest request) => await outcomeHandler.Handle(request);
    
    public async Task<SampleSpace> Send(AddSampleSpaceRequest request) => await sampleSpaceHandler.Handle(request);

    public async Task<PEvent> Send(AddPEventRequest request) => await eventHandler.Handle(request);

    public async Task<AtomicEvent> Send(AddAtomicEventRequest request) => await atomicEventHandler.Handle(request);
}
