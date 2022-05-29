using System.Threading.Tasks;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public static implicit operator Task<Result<TSuccess, TFailure>>(AsyncResultFlow<TSuccess, TFailure> flow)
        =>
        flow.ToTask();
}