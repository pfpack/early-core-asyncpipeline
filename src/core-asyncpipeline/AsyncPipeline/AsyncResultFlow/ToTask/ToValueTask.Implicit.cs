#nullable enable

using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public static implicit operator Task<Result<TSuccess, TFailure>>(AsyncResultFlow<TSuccess, TFailure> pipeline)
            =>
            pipeline.ToTask();
    }
}