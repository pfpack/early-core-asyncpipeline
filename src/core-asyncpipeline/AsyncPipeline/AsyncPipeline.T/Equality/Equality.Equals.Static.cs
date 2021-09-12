#nullable enable

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public static bool Equals(AsyncPipeline<T> left, AsyncPipeline<T> right)
            =>
            left.Equals(right);
    }
}