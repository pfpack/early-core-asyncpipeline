namespace System;

partial struct AsyncPipeline<T>
{
    public override int GetHashCode()
        =>
        isStopped is false ? NonStoppedHashCode() : StoppedHashCode();

    private int NonStoppedHashCode()
        =>
        HashCode.Combine(
            EqualityContractHashCode(),
            ValueTaskComparer.GetHashCode(valueTask),
            ConfigurationComparer.GetHashCode(Configuration),
            CancellationTokenComparer.GetHashCode(cancellationToken));

    private static int StoppedHashCode()
        =>
        HashCode.Combine(
            EqualityContractHashCode());

    private static int EqualityContractHashCode()
        =>
        EqualityContractComparer.GetHashCode(EqualityContract);
}