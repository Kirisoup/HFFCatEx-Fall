using System;
using System.Collections.Generic;
using System.Linq;

namespace fall {
    public static class Extensions {
        public static void ForEach<T>(this T[] array, Action<T> action) {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            foreach (var item in array) {
                action(item);
            }
        }

        public static IEnumerable<T> AnyOr<T>(this IEnumerable<T> self, IEnumerable<T> other) =>
            self.Any() ? self : other;

        public static UnityEngine.Vector3 ToBottom(this UnityEngine.Vector3 v) =>
            new(v.x, LevelVoid.bottomY, v.z);
    }

}