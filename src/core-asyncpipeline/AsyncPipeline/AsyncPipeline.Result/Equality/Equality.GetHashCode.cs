namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public override int GetHashCode()
        =>
        HashCode.Combine(
            EqualityContractComparer.GetHashCode(EqualityContract),
            AsyncPipelineComparer.GetHashCode(pipeline));
}