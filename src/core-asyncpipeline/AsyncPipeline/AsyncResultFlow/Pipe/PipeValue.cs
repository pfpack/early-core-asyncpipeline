#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> PipeValue<TResult>(
            Func<Result<TSuccess, TFailure>, CancellationToken, ValueTask<TResult>> pipeAsync)
            =>
            asyncPipeline.InternalPipeValue(
                pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));
    }
}