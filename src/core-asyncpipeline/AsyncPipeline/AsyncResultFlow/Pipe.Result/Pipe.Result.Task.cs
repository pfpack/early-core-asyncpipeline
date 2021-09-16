#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TResultFailure> Pipe<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, CancellationToken, Task<Result<TResultSuccess, TResultFailure>>> pipeAsync)
            where TResultFailure : struct
            =>
            InnerPipe(
                pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

        private AsyncResultFlow<TResultSuccess, TResultFailure> InnerPipe<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, CancellationToken, Task<Result<TResultSuccess, TResultFailure>>> pipeAsync)
            where TResultFailure : struct
            =>
            new(
                asyncPipeline.InternalPipe(pipeAsync));
    }
}