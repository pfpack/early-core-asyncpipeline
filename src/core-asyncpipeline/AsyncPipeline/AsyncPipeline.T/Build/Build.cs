using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public Task<T> Build()
        =>
        isStopped is false ? valueTask.AsTask() : Task.FromCanceled<T>(CanceledToken());

    public ValueTask<T> BuildValue()
        =>
        isStopped is false ? valueTask : ValueTask.FromCanceled<T>(CanceledToken());
}