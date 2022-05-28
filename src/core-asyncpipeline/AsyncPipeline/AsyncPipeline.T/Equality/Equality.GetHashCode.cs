namespace System;

partial struct AsyncPipeline<T>
{
    public override int GetHashCode()
        =>
        isCanceled is false ? NonCanceledHashCode() : CanceledHashCode();

    private int NonCanceledHashCode()
        =>
        HashCode.Combine(
            EqualityContractHashCode(),
            ValueTaskComparer.GetHashCode(valueTask),
            CancellationTokenComparer.GetHashCode(cancellationToken));

    private int CanceledHashCode()
        =>
        HashCode.Combine(
            EqualityContractHashCode());

    private static int EqualityContractHashCode()
        =>
        EqualityContractComparer.GetHashCode(EqualityContract);
}