using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TResultFailure> PipeValue<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> pipeAsync)
            where TResultFailure : struct
            =>
            InnerPipeValue(
                pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

        private AsyncResultFlow<TResultSuccess, TResultFailure> InnerPipeValue<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> pipeAsync)
            where TResultFailure : struct
            =>
            new(
                asyncPipeline.InternalPipeValue(pipeAsync));
    }
}