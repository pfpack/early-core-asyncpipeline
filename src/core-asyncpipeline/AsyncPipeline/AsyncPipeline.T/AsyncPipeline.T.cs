#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
    {
        private static CancellationToken CanceledToken() => new(canceled: true);

        private readonly ValueTask<T> valueTask;

        private readonly bool isCanceled;

        private readonly CancellationToken cancellationToken;

        internal AsyncPipeline(ValueTask<T> valueTask, bool isCanceled, CancellationToken cancellationToken)
        {
            this.valueTask = valueTask;
            this.isCanceled = isCanceled;
            this.cancellationToken = cancellationToken;
        }
    }
}
