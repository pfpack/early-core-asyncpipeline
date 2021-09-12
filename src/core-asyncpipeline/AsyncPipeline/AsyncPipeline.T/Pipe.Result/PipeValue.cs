#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncResultFlow<TSuccess, TFailure> PipeValue<TSuccess, TFailure>(
            Func<T, CancellationToken, ValueTask<Result<TSuccess, TFailure>>> nextAsync)
            where TFailure : struct
            =>
            new(
                InternalPipeValue(
                    nextAsync ?? throw new ArgumentNullException(nameof(nextAsync))));
    }
}