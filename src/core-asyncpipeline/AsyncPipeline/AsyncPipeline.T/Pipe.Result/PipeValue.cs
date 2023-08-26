using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<TSuccess, TFailure> PipeValue<TSuccess, TFailure>(
        Func<T, CancellationToken, ValueTask<Result<TSuccess, TFailure>>> pipeAsync)
        where TFailure : struct
        =>
        new(
            InternalPipeValue(
                pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync))));
}