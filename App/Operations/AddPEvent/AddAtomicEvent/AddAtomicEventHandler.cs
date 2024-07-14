using App.Entities;
using App.Operations.Interfaces;
using ProbSharp.Persistence;

namespace App.Operations.AddPEvent.AddAtomicEvent;

public class AddAtomicEventHandler(
        ProbSharpContext context,
        INodeFactory<AddAtomicEventRequest> nodeFactory, 
        IRelationshipFactory<AddAtomicEventRequest> relationshipFactory
    ) : IRequestHandler<AddAtomicEventRequest, AtomicEvent>
{
    public async Task<AtomicEvent> Handle(AddAtomicEventRequest request)
    {
        return default;
    }
}
