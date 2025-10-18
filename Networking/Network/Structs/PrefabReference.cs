using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS.Networking
{
    [Serializable]
    public abstract class PrefabReference
    {
        public abstract object PrefabKey {
            get;set;
        }
    }

    [Serializable]
    public class PrefabReferenceT<T> : PrefabReference {
        public T prefabKey;

        public override object PrefabKey {
            get => prefabKey;
            set => prefabKey = (T)value;
        }
    }

    public enum DespawnMode : ushort {
        Destroy = 0,
        Pool = 1,
    }
}
