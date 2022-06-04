using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public static implicit operator Task<Result<TSuccess, TFailure>>(AsyncPipeline<TSuccess, TFailure> flow)
        =>
        flow.ToTask();
}