using App.Entities;
using App.Operations.AddOutcome;
using App.Operations.AddPEvent;
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
            .AddTransient<IRequestHandler<AddOutcomeRequest, Outcome>, AddOutcomeHandler>()
            .AddTransient<IRequestHandler<AddPEventRequest, PEvent>, AddPEventHandler>();

        serviceCollection
            .AddTransient<INodeFactory<AddSampleSpaceRequest>, AddSampleSpaceFactory>()
            .AddTransient<INodeFactory<AddOutcomeRequest>, AddOutcomeFactory>()
            .AddTransient<IRelationshipFactory<AddOutcomeRequest>, AddOutcomeFactory>();

        serviceCollection
            .AddScoped<IValidator<AddPEventRequest>, AddPEventRequestValidator>();
    }
}
