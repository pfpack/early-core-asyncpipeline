#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> PipeValue<TResult>(
            Func<Result<TSuccess, TFailure>, CancellationToken, ValueTask<TResult>> foldAsync)
            =>
            asyncPipeline.InternalPipeValue(
                foldAsync ?? throw new ArgumentNullException(nameof(foldAsync)));
    }
}