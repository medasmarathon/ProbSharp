using App.Entities;
using App.Operations;
using App.Operations.AddOutcome;
using App.Operations.AddPEvent;
using App.Operations.AddPEvent.AddAndEvent;
using App.Operations.AddPEvent.AddAtomicEvent;
using App.Operations.AddPEvent.AddOrEvent;
using App.Operations.AddSampleSpace;
using App.Operations.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace App;

public static class Registrations
{
    public static void RegisterProbSharpApp(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediator();
        serviceCollection
            .AddTransient<INodeFactory<AddSampleSpaceRequest>, AddSampleSpaceFactory>();

        serviceCollection
            .AddTransient<INodeFactory<AddOutcomeRequest>, AddOutcomeFactory>()
            .AddTransient<IRelationshipFactory<AddOutcomeRequest>, AddOutcomeFactory>()
            ;

        serviceCollection
            .AddTransient<INodeFactory<AddAtomicEventRequest>, AddAtomicEventFactory>()
            .AddTransient<IRelationshipFactory<AddAtomicEventRequest>, AddAtomicEventFactory>()
            ;

        serviceCollection
            .AddTransient<IRelationshipFactory<AddAndEventRequest>, AddAndEventFactory>()
            .AddTransient<INodeFactory<AddAndEventRequest>, AddAndEventFactory>()
            ;

        serviceCollection
            .AddTransient<IRelationshipFactory<AddOrEventRequest>, AddOrEventFactory>()
            .AddTransient<INodeFactory<AddOrEventRequest>, AddOrEventFactory>()
            ;

        serviceCollection
            .AddScoped<IValidator<AddPEventRequest>, AddPEventRequestValidator>()
            ;
    }
}
