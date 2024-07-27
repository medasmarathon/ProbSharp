using App.Entities;
using App.Operations;
using App.Operations.AddOutcome;
using App.Operations.AddPEvent;
using App.Operations.AddPEvent.AddAndEvent;
using App.Operations.AddPEvent.AddAtomicEvent;
using App.Operations.AddSampleSpace;
using App.Operations.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace App;

public static class Registrations
{
    public static void RegisterProbSharpApp(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddTransient<IRequestHandler<AddSampleSpaceRequest, SampleSpace>, AddSampleSpaceHandler>()
            .AddTransient<INodeFactory<AddSampleSpaceRequest>, AddSampleSpaceFactory>();


        serviceCollection
            .AddTransient<IRequestHandler<AddOutcomeRequest, Outcome>, AddOutcomeHandler>()
            .AddTransient<INodeFactory<AddOutcomeRequest>, AddOutcomeFactory>()
            .AddTransient<IRelationshipFactory<AddOutcomeRequest>, AddOutcomeFactory>()
            ;

        serviceCollection
            .AddTransient<IRequestHandler<AddPEventRequest, PEvent>, AddPEventHandler>()
            ;
            
        serviceCollection
            .AddTransient<IRequestHandler<AddAtomicEventRequest, AtomicEvent>, AddAtomicEventHandler>()
            .AddTransient<INodeFactory<AddAtomicEventRequest>, AddAtomicEventFactory>()
            .AddTransient<IRelationshipFactory<AddAtomicEventRequest>, AddAtomicEventFactory>()
            ;

        serviceCollection
            .AddTransient<IRequestHandler<AddAndEventRequest, AndEvent>, AddAndEventHandler>()
            .AddTransient<IRelationshipFactory<AddAndEventRequest>, AddAndEventFactory>()
            .AddTransient<INodeFactory<AddAndEventRequest>, AddAndEventFactory>()
            ;

        serviceCollection
            .AddScoped<IValidator<AddPEventRequest>, AddPEventRequestValidator>()
            ;

        serviceCollection
            .AddTransient<Operator>()
            ;
    }
}
