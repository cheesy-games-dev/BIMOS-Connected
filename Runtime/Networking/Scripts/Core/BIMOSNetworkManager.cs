using KadenZombie8.BIMOS.Rig.Spawning;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    public class BIMOSNetworkManager : NetworkManager {
        public static BIMOSNetworkManager Singleton {
            get; private set;
        }
        public NetworkRig LocalRig {
            get; internal set;
        }
        public Dictionary<NetworkConnectionToClient, NetworkRig> RigCache {
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
        public override void Awake() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            Singleton = this;
            int layer = LayerMask.GetMask("BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            if (AutoHost) {
                StartHost();
            }
        }
        public override void OnStartServer() {
            base.OnStartServer();
            _defaultSpawnPoint = SpawnPointManager.Instance.SpawnPoint.transform;
            playerPrefab = null;
            autoCreatePlayer = true;        
            NetworkServer.ReplaceHandler<AddPlayerMessage>((x, y) => OnServerAddPlayer(x));
        }

        [Server]
        public override void OnServerAddPlayer(NetworkConnectionToClient conn) {
            Transform spawn = DynamicSpawn ? SpawnPointManager.Instance.SpawnPoint.transform : _defaultSpawnPoint;
            var gameobject = Instantiate(RigPrefab, spawn.position, spawn.rotation).gameObject;
            NetworkServer.AddPlayerForConnection(conn, gameobject);
        }
    }
}
