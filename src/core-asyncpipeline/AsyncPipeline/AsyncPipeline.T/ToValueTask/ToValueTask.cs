using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public ValueTask<T> ToValueTask()
        =>
        isCanceled is false ? valueTask : InnerCanceledToken().InternalPipe(ValueTask.FromCanceled<T>);
}