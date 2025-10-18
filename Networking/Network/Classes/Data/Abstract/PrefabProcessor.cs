using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public abstract class PrefabProcessor : MonoBehaviour {
        public static PrefabProcessor singleton;
        protected virtual void Start() {
            singleton = this;
            InitSingleton();
        }
        public abstract void InitSingleton();
        public abstract void Spawn(GameObject newInstance);
        public abstract void Despawn(GameObject instance);
    }

    public abstract class PrefabProcessorT<T> : PrefabProcessor where T : PrefabReference {
        public List<NetworkBarcodeT<T>> PrefabList = new();
        public Dictionary<byte, GameObject> SpawnedPrefabs = new();
    }
}
