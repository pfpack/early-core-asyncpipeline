using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public Task<T> ToTask()
        =>
        isCanceled is false ? valueTask.AsTask() : InnerCanceledTask();

    private Task<T> InnerCanceledTask()
        =>
        Task.FromCanceled<T>(cancellationToken.IsCancellationRequested ? cancellationToken : CanceledToken());
}