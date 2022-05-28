namespace System;

partial struct AsyncPipeline<T>
{
    public override int GetHashCode()
        =>
        HashCode.Combine(
            EqualityContractComparer.GetHashCode(EqualityContract),
            IsCanceledComparer.GetHashCode(isCanceled),
            ValueTaskComparer.GetHashCode(valueTask),
            CancellationTokenComparer.GetHashCode(cancellationToken));
}