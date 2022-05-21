using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public ValueTask<T> ToValueTask()
        =>
        isCanceled is false ? valueTask : CanceledToken().InternalPipe(ValueTask.FromCanceled<T>);
}