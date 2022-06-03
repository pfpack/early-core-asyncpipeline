using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResult> Pipe<TResult>(Func<Result<TSuccess, TFailure>, CancellationToken, Task<TResult>> pipeAsync)
        =>
        asyncPipeline.InternalPipe(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));
}