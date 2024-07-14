namespace App.Operations.Interfaces;

public interface IOperator<TRequest, TResponse>
{
    Task<TResponse> Send(TRequest request);
}
