using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public ValueTask<T> ToValueTask()
        =>
        isStopped is false ? valueTask : ValueTask.FromCanceled<T>(CanceledToken());
}