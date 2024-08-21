using App.Operations.AddPEvent;
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
