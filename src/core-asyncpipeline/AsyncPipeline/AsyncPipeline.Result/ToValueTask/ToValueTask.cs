using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public ValueTask<Result<TSuccess, TFailure>> ToValueTask()
        =>
        asyncPipeline.ToValueTask();
}