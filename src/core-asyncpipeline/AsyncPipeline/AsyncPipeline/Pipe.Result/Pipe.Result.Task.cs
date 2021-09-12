#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial class AsyncPipeline
    {
        public static AsyncPipeline<T> Pipe<T>(Task<T> task, CancellationToken cancellationToken = default)
            =>
            InnerPipe(
                task: task ?? throw new ArgumentNullException(nameof(task)),
                cancellationToken: cancellationToken);

        private static AsyncPipeline<T> InnerPipe<T>(Task<T> task, CancellationToken cancellationToken)
            =>
            new(
                valueTask: new(task),
                hasCanceled: cancellationToken.IsCancellationRequested,
                cancellationToken: cancellationToken);
    }
}