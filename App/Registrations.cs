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
            .AddScoped<IValidator<AddPEventRequest>, AddPEventRequestValidator>()
            ;
    }
}
