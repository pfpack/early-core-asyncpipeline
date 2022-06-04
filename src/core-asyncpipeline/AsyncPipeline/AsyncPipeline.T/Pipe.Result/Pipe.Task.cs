using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncPipeline<TSuccess, TFailure> Pipe<TSuccess, TFailure>(
            Func<T, CancellationToken, Task<Result<TSuccess, TFailure>>> pipeAsync)
            where TFailure : struct
            =>
            new(
                InternalPipe(
                    pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync))));
    }
}