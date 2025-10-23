using System;
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
    }
}
