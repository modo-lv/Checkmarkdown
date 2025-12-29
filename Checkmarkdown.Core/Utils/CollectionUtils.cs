namespace Checkmarkdown.Core.Utils;

public static class CollectionUtils {
    extension(Object target) {
        public Boolean IsIn(params Object[] candidates) =>
            candidates.Contains(target);
    }
}