using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResult> Pipe<TResult>(Func<Result<TSuccess, TFailure>, Task<TResult>> pipeAsync)
        =>
        pipeline.InternalPipe(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));
}