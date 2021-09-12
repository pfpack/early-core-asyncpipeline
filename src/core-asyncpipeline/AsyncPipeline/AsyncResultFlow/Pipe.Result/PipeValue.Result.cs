#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TResultFailure> PipeValue<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> mapAsync)
            where TResultFailure : struct
            =>
            InnerPipeValue(
                mapAsync ?? throw new ArgumentNullException(nameof(mapAsync)));

        private AsyncResultFlow<TResultSuccess, TResultFailure> InnerPipeValue<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> mapAsync)
            where TResultFailure : struct
            =>
            new(
                asyncPipeline.InternalPipeValue(mapAsync));
    }
}