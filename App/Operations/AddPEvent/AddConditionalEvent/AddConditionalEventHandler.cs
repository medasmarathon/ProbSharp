using App.Entities;
using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddConditionalEvent;

public class AddConditionalEventHandler(
        ProbSharpContext context,
        IMediator mediator
    ) : IRequestHandler<AddConditionalEventRequest, ConditionalEvent>
{
    public ValueTask<ConditionalEvent> Handle(AddConditionalEventRequest request, CancellationToken cancellationToken)
    {
        return default;
    }
}