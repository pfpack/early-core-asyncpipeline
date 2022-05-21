using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public static implicit operator ValueTask<T>(AsyncPipeline<T> pipeline)
        =>
        pipeline.ToValueTask();
}