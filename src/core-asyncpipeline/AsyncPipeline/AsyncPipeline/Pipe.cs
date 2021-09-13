#nullable enable

using System.Threading;

namespace System
{
    partial class AsyncPipeline
    {
        public static AsyncPipeline<T> Pipe<T>(T value, CancellationToken cancellationToken = default)
            =>
            InnerPipe(
                value, cancellationToken);

        private static AsyncPipeline<T> InnerPipe<T>(T value, CancellationToken cancellationToken)
            =>
            new(
                valueTask: new(value),
                isCanceled: cancellationToken.IsCancellationRequested,
                cancellationToken: cancellationToken);
    }
}