#nullable enable

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public bool Equals(AsyncPipeline<T> other)
            =>
            ValueTaskComparer.Equals(valueTask, other.valueTask)
            && HasCanceledComparer.Equals(hasCanceled, other.hasCanceled)
            && CancellationTokenComparer.Equals(cancellationToken, other.cancellationToken);
    }
}