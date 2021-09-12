#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncResultFlow<TSuccess, TFailure> Pipe<TSuccess, TFailure>(
            Func<T, CancellationToken, Task<Result<TSuccess, TFailure>>> nextAsync)
            where TFailure : struct
            =>
            new(
                InternalPipe(
                    nextAsync ?? throw new ArgumentNullException(nameof(nextAsync))));
    }
}