using App.Entities;
using App.Operations.AddOutcome;
using App.Operations.AddPEvent;
using App.Operations.AddPEvent.AddAndEvent;
using App.Operations.AddPEvent.AddOrEvent;
using App.Operations.AddPEvent.AddAtomicEvent;
using App.Operations.AddSampleSpace;
using App.Operations.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace App.Operations;

public class Operator(
        IServiceProvider services
    )
{
    public async Task<Outcome> Send(AddOutcomeRequest request) => 
        await services.GetRequiredService<IRequestHandler<AddOutcomeRequest, Outcome>>().Handle(request);
    
    public async Task<SampleSpace> Send(AddSampleSpaceRequest request) => 
        await services.GetRequiredService<IRequestHandler<AddSampleSpaceRequest, SampleSpace>>().Handle(request);

    public async Task<PEvent> Send(AddPEventRequest request) => 
        await services.GetRequiredService<IRequestHandler<AddPEventRequest, PEvent>>().Handle(request);

    public async Task<AtomicEvent> Send(AddAtomicEventRequest request) => 
        await services.GetRequiredService<IRequestHandler<AddAtomicEventRequest, AtomicEvent>>().Handle(request);
    
    public async Task<AndEvent> Send(AddAndEventRequest request) => 
        await services.GetRequiredService<IRequestHandler<AddAndEventRequest, AndEvent>>().Handle(request);
        
    public async Task<OrEvent> Send(AddOrEventRequest request) => 
        await services.GetRequiredService<IRequestHandler<AddOrEventRequest, OrEvent>>().Handle(request);
}
