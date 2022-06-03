using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public Task<Result<TSuccess, TFailure>> ToTask()
        =>
        pipeline.ToTask();
}