using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    // TODO: Consider to decide to keep the obsoletes or not to keep

    [Obsolete("This method is obsolete. Call Build instead.", error: true)]
    public Task<T> ToTask()
        =>
        Build();

    [Obsolete("This method is obsolete. Call BuildValuePreserved instead.", error: true)]
    public ValueTask<T> ToValueTaskPreserved()
        =>
        BuildValuePreserved();

    [Obsolete("This method is obsolete. Call BuildValue instead.", error: true)]
    public ValueTask<T> ToValueTask()
        =>
        BuildValue();

    [Obsolete("This operator is obsolete. Call Build instead.", error: true)]
    public static implicit operator Task<T>(AsyncPipeline<T> pipeline)
        =>
        pipeline.Build();

    [Obsolete("This operator is obsolete. Call BuildValue or BuildValuePreserved instead.", error: true)]
    public static implicit operator ValueTask<T>(AsyncPipeline<T> pipeline)
        =>
        pipeline.BuildValuePreserved(); // Preserved is used in the operator
}