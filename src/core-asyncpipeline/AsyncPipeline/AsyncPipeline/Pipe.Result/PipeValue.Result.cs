#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial class AsyncPipeline
    {
        public static AsyncResultFlow<TSuccess, TFailure> PipeValue<TSuccess, TFailure>(
            ValueTask<Result<TSuccess, TFailure>> valueTask,
            CancellationToken cancellationToken = default)
            where TFailure : struct
            =>
            new(
                asyncPipeline: InnerPipeValue(valueTask, cancellationToken));
    }
}