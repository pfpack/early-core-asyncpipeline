namespace System;

partial struct ConfigurableAsyncPipeline
{
    public override int GetHashCode()
        =>
        HashCode.Combine(
            EqualityContractComparer.GetHashCode(EqualityContract),
            ConfigurationComparer.GetHashCode(Configuration));
}