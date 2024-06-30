using ProbSharp.Persistence;

namespace App.Operations.Interfaces;

public interface INodeFactory<TRequest>
{
    Node CreateNode(TRequest request);
}

public interface IRelationshipFactory<TRequest>
{
    Relationship CreateRelationship(TRequest request);
}

