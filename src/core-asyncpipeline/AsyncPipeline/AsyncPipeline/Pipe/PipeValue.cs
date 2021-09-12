#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial class AsyncPipeline
    {
        public static AsyncPipeline<T> PipeValue<T>(ValueTask<T> valueTask, CancellationToken cancellationToken = default)
            =>
            InnerPipeValue(
                valueTask, cancellationToken);

        private static AsyncPipeline<T> InnerPipeValue<T>(ValueTask<T> valueTask, CancellationToken cancellationToken)
            =>
            new(
                valueTask: valueTask,
                hasCanceled: cancellationToken.IsCancellationRequested,
                cancellationToken: cancellationToken);
    }
}