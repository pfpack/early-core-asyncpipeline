using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public Task<Result<TSuccess, TFailure>> Build()
        =>
        pipeline.Build();

    public ValueTask<Result<TSuccess, TFailure>> BuildValue()
        =>
        pipeline.BuildValue();
}