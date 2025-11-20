using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KadenZombie8.BIMOS
{
    public static class UnityExtensions
    {
        public static T AddComponent<T>(this Component component) where T : Component {
            return component.gameObject.AddComponent<T>();
        }
        public static Component AddComponent(this Component component, Type componentType) {
            return component.gameObject.AddComponent(componentType);
        }

        public static T GetRandomItem<T>(this IEnumerable<T> enumerable) {
            var array = enumerable.ToArray();
            var index = UnityEngine.Random.Range(0, array.Length - 1);
            return array[index];
        }

        public static Rigidbody CopyFromTo(this Rigidbody from, Rigidbody to) {
            from.position = to.position;
            from.rotation = to.rotation;
            return from;
        }

        public static Transform CopyFromTo(this Transform from, Transform to) {
            from.position = to.position;
            from.rotation = to.rotation;
            return from;
        }
    }
}
