namespace Checkmarkdown.Core.Utils;

public static class NullUtils
{
    extension<T>(T? value)
    {
        /// <param name="orError">Custom message for the exception thrown on a <c>null</c>.</param>
        /// <returns><paramref name="value"/> unless it's <c>null</c>.</returns>
        /// <exception cref="NullReferenceException">If <paramref name="value"/> is <c>null</c></exception>
        public T NotNull(String? orError) {
            return value ?? throw new NullReferenceException(orError);
        }
    }
}