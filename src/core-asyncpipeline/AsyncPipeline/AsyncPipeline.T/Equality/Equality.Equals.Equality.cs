#nullable enable

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public static bool operator ==(AsyncPipeline<T> left, AsyncPipeline<T> right)
            =>
            left.Equals(right);

        public static bool operator !=(AsyncPipeline<T> left, AsyncPipeline<T> right)
            =>
            left.Equals(right) is false;
    }
}