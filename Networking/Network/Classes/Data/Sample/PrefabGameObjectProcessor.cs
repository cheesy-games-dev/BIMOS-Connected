using Riptide;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class PrefabGameObjectProcessor : PrefabProcessorT<PrefabReferenceT<NetworkEntityHost>> {
        public new static PrefabGameObjectProcessor singleton;

        public override void InitSingleton() {
            singleton = this;
        }

        public override void Spawn(GameObject newInstance) {
            var entity = newInstance.GetNetworkEntity().Entity;
            var barcode = PrefabList.Find(x=>x.reference.prefabKey.Entity.barcode == entity.barcode);
            Message spawnMessage = Message.Create(MessageSendMode.Reliable, ServerToClient.SpawnMessage);

            SpawnedPrefabs.Add(entity.NetId, newInstance);
        }
        public override void Despawn(GameObject instance) {
            SpawnedPrefabs.Remove(instance);
        }
    }
}
