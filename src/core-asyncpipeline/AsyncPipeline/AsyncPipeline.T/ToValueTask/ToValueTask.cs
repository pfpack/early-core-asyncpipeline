using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public ValueTask<T> ToValueTask()
        =>
        isStopped is false ? task : ValueTask.FromCanceled<T>(CanceledToken());
}