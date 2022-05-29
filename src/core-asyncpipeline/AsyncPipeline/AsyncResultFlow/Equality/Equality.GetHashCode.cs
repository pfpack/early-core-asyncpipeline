namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public override int GetHashCode()
        =>
        HashCode.Combine(
            EqualityContractComparer.GetHashCode(EqualityContract),
            AsyncPipelineComparer.GetHashCode(asyncPipeline));
}