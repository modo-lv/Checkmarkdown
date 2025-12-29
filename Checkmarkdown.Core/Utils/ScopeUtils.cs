namespace Checkmarkdown.Core.Utils;

public static class ScopeUtils {
    extension<TReceiver>(TReceiver receiver) {
        /// <summary>
        /// Runs <paramref name="action"/> with the the receiver as parameter, and returns the receiver. 
        /// </summary>
        public TReceiver Also(Action<TReceiver> action) {
            action(receiver);
            return receiver;
        }

        /// <summary>Runs <paramref name="transform"/> on the receiver and returns the result.</summary>
        public TResult Let<TResult>(Func<TReceiver, TResult> transform) {
            return transform(receiver); 
        }
    }
}