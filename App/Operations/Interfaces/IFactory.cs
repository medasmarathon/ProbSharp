using ProbSharp.Persistence;

namespace App.Operations.Interfaces;

public interface INodeFactory<TRequest>
{
    List<Node> CreateNodes(TRequest request);
}

public interface IRelationshipFactory<TRequest>
{
    List<Relationship> CreateRelationships(TRequest request);
}

