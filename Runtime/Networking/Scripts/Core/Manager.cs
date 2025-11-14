using FishNet;
using FishNet.Connection;
using FishNet.Object;
using KadenZombie8.BIMOS.Rig.Spawning;
using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    public class Manager : MonoBehaviour {
        public static Manager singleton {
            get; private set;
        }
        public NetworkRig LocalRig {
            get; internal set;
        }
        public Dictionary<NetworkConnection, NetworkRig> RigCache {
            get; internal set;
        }
        private Transform _defaultSpawnPoint;

        [Header("BIMOS"), Tooltip("Standard Rig Prefab:\n" +
            "Clients spawn as Rig Prefab set by the Server")]
        public NetworkRig RigPrefab;
        [Tooltip("Dynamic Spawn:\n" +
            "True: New Clients spawn at the Servers last spawn point\n" +
            "False: New Clients spawn at the Servers first spawn point")]
        public bool DynamicSpawn;
        [Tooltip("Auto Host:\n" +
           "Create Server when Game Loads")]
        public bool AutoHost = true;
        private void Awake() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            singleton = this;
            int layer = LayerMask.GetMask("BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
        }
        private void Start() {
            _defaultSpawnPoint = SpawnPointManager.Instance.SpawnPoint.transform;
            if (AutoHost) {
                InstanceFinder.ServerManager.StartConnection();
                InstanceFinder.ClientManager.StartConnection();
            }
            InstanceFinder.SceneManager.OnClientLoadedStartScenes += OnCreateRig;
        }

        [Server]
        private void OnCreateRig(NetworkConnection conn, bool asServer) {
            Transform spawn = DynamicSpawn ? SpawnPointManager.Instance.SpawnPoint.transform:_defaultSpawnPoint;
            NetworkObject nob = InstanceFinder.NetworkManager.GetPooledInstantiated(RigPrefab, spawn.position, spawn.rotation, true);
            InstanceFinder.ServerManager.Spawn(nob, conn);
        }
    }
}
