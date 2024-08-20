using Mediator;
using ProbSharp.Persistence;

namespace App.Operations.Common;

public class CreateNodesFor<T>(T entity) : IRequest<List<Node>>
{
    public T Entity = entity;
}
