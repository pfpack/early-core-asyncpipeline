using System.Threading.Tasks;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public Task<Result<TSuccess, TFailure>> ToTask()
        =>
        asyncPipeline.ToTask();
}