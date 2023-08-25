using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TResultFailure> Pipe<TResultSuccess, TResultFailure>(
        Func<Result<TSuccess, TFailure>, CancellationToken, Task<Result<TResultSuccess, TResultFailure>>> pipeAsync)
        where TResultFailure : struct
        =>
        InnerPipe(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

    private AsyncPipeline<TResultSuccess, TResultFailure> InnerPipe<TResultSuccess, TResultFailure>(
        Func<Result<TSuccess, TFailure>, CancellationToken, Task<Result<TResultSuccess, TResultFailure>>> pipeAsync)
        where TResultFailure : struct
        =>
        new(
            pipeline.InternalPipe(pipeAsync));
}