using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    // TODO: Consider to decide to keep the obsoletes or not to keep

    [Obsolete("This method is obsolete. Call Build instead.", error: true)]
    public Task<Result<TSuccess, TFailure>> ToTask()
        =>
        Build();

    [Obsolete("This method is obsolete. Call BuildValuePreserved instead.", error: true)]
    public ValueTask<Result<TSuccess, TFailure>> ToValueTaskPreserved()
        =>
        BuildValuePreserved();

    [Obsolete("This method is obsolete. Call BuildValue instead.", error: true)]
    public ValueTask<Result<TSuccess, TFailure>> ToValueTask()
        =>
        BuildValue();

    [Obsolete("This operator is obsolete. Call Build instead.", error: true)]
    public static implicit operator Task<Result<TSuccess, TFailure>>(AsyncPipeline<TSuccess, TFailure> pipeline)
        =>
        pipeline.Build();

    [Obsolete("This operator is obsolete. Call BuildValue or BuildValuePreserved instead.", error: true)]
    public static implicit operator ValueTask<Result<TSuccess, TFailure>>(AsyncPipeline<TSuccess, TFailure> pipeline)
        =>
        pipeline.BuildValuePreserved(); // Preserved is used in the operator
}