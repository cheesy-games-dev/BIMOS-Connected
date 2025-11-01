using FishNet;
using FishNet.Connection;
using KadenZombie8.BIMOS.Rig.Spawning;
using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    public class NetworkManager : MonoBehaviour {
        public static NetworkManager Instance {
            get; private set;
        }
        public NetworkRig LocalRig {
            get; internal set;
        }
        public Dictionary<NetworkConnection, NetworkRig> RigCache {
            get; internal set;
        }
        public NetworkRig RigPrefab;
        public bool DynamicSpawn;
        private Transform _defaultSpawnPoint;

        private void Start() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            Instance = this;
            int layer = LayerMask.GetMask("Player", "BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            _defaultSpawnPoint = SpawnPointManager.Instance.SpawnPoint.transform;
            InstanceFinder.SceneManager.OnClientLoadedStartScenes += PlayerJoined;
        }

        public void PlayerJoined(NetworkConnection conn, bool asServer) {
            Transform spawn = DynamicSpawn ? SpawnPointManager.Instance.SpawnPoint.transform:_defaultSpawnPoint;
            var player = InstanceFinder.NetworkManager.GetPooledInstantiated(RigPrefab, spawn.position, spawn.rotation, true);
            InstanceFinder.ServerManager.Spawn(player, conn);
        }
    }
}
