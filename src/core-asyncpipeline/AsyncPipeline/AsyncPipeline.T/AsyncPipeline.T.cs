#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
    {
        private static readonly CancellationToken CanceledToken;

        static AsyncPipeline() => CanceledToken = new(canceled: true);

        private readonly ValueTask<T> valueTask;

        private readonly bool hasCanceled;

        private readonly CancellationToken cancellationToken;

        internal AsyncPipeline(ValueTask<T> valueTask, bool hasCanceled, CancellationToken cancellationToken)
        {
            this.valueTask = valueTask;
            this.cancellationToken = cancellationToken;
            this.hasCanceled = hasCanceled;
        }
    }
}