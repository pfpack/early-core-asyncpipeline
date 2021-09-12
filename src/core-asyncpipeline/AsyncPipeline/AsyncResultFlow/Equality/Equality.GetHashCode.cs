#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public override int GetHashCode() => HashCode.Combine(EqualityContract, AsyncPipelineComparer.GetHashCode(asyncPipeline));
    }
}