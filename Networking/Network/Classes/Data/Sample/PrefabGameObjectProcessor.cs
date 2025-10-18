using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class PrefabGameObjectProcessor : PrefabProcessorT<GameObject> {
        public new static PrefabGameObjectProcessor singleton;

        public override void InitSingleton() {
            singleton = this;
        }

        public override void Spawn(GameObject newInstance) {
            var barcode = PrefabList.Find(x=>x.reference.prefabKey.GetComponent<GameObjectBarcode>().Barcode == newInstance.GetComponent<GameObjectBarcode>().Barcode);
            SpawnedPrefabs.Add(newInstance);
        }
        public override void Despawn(GameObject instance) {
            SpawnedPrefabs.Remove(instance);
        }
    }
}
