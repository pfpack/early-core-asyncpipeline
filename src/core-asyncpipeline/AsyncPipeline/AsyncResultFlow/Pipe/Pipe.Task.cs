#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> Pipe<TResult>(
            Func<Result<TSuccess, TFailure>, CancellationToken, Task<TResult>> foldAsync)
            =>
            asyncPipeline.InternalPipe(
                foldAsync ?? throw new ArgumentNullException(nameof(foldAsync)));
    }
}