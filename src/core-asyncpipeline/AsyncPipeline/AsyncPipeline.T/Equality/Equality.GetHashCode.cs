namespace System;

partial struct AsyncPipeline<T>
{
    public override int GetHashCode()
        =>
        isCanceled is false ? NonCanceledHashCode() : AsyncPipeline<T>.CanceledHashCode();

    private int NonCanceledHashCode()
        =>
        HashCode.Combine(
            EqualityContractHashCode(),
            ValueTaskComparer.GetHashCode(valueTask),
            CancellationTokenComparer.GetHashCode(cancellationToken));

    private static int CanceledHashCode()
        =>
        HashCode.Combine(
            EqualityContractHashCode());

    private static int EqualityContractHashCode()
        =>
        EqualityContractComparer.GetHashCode(EqualityContract);
}