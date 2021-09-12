#nullable enable

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public override int GetHashCode()
            =>
            HashCode.Combine(
                EqualityContract,
                ValueTaskComparer.GetHashCode(valueTask),
                HasCanceledComparer.GetHashCode(hasCanceled),
                CancellationTokenComparer.GetHashCode(cancellationToken));
    }
}