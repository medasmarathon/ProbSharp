using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.Common;

public class CreateRelatedRelationshipsFor<T>(T entity) : IRequest<List<Relationship>>
{
    public T Entity = entity;
}
