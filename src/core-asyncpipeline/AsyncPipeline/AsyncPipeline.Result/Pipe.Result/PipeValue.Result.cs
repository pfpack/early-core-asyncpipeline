using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TResultFailure> PipeValue<TResultSuccess, TResultFailure>(
        Func<Result<TSuccess, TFailure>, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> pipeAsync)
        where TResultFailure : struct
        =>
        InnerPipeValue(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

    private AsyncPipeline<TResultSuccess, TResultFailure> InnerPipeValue<TResultSuccess, TResultFailure>(
        Func<Result<TSuccess, TFailure>, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> pipeAsync)
        where TResultFailure : struct
        =>
        new(
            pipeline.InternalPipeValue(pipeAsync));
}