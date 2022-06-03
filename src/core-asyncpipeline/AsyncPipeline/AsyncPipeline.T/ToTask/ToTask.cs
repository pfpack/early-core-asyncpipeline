using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public Task<T> ToTask()
        =>
        isStopped is false ? task.AsTask() : Task.FromCanceled<T>(CanceledToken());
}