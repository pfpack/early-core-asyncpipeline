using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public ValueTask<T> ToValueTask()
        =>
        isCanceled is false ? valueTask : InnerCanceledValueTask();

    private ValueTask<T> InnerCanceledValueTask()
        =>
        cancellationToken.IsCancellationRequested
        ? ValueTask.FromCanceled<T>(cancellationToken)
        : ValueTask.FromCanceled<T>(CanceledToken());
}