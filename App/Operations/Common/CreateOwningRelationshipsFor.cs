using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.Common;

public class CreateOwningRelationshipsFor<T>(T entity) : IRequest<List<Relationship>>
{
    public T Entity = entity;
}
