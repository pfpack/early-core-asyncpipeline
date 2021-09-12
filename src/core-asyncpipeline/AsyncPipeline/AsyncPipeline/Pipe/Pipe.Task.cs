#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial class AsyncPipeline
    {
        public static AsyncResultFlow<TSuccess, TFailure> Pipe<TSuccess, TFailure>(
            Task<Result<TSuccess, TFailure>> task, CancellationToken cancellationToken = default)
            where TFailure : struct
            =>
            new(
                asyncPipeline: InnerPipe(
                    task ?? throw new ArgumentNullException(nameof(task)),
                    cancellationToken));
    }
}