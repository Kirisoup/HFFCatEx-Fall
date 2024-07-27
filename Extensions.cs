using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions {
    public static void ForEach<T>(this T[] array, Action<T> action) {
        foreach (var item in array ?? throw new ArgumentNullException(nameof(array))) {
            (action ?? throw new ArgumentNullException(nameof(action)))(item);
        }
    }

    public static IEnumerable<T> AnyOr<T>(this IEnumerable<T> self, IEnumerable<T> other) =>
        (self ?? throw new ArgumentNullException(nameof(self)))
            .Any() ? self
                : (other ?? throw new ArgumentNullException(nameof(other)));
}
