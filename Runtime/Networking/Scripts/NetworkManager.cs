using KadenZombie8.BIMOS.Rig.Spawning;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    public class NetworkManager : Mirror.NetworkManager {
        public static new NetworkManager singleton {
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
        [Tooltip("Rig Info:\n" +
           "Local Rig Info the Client sends to the Server")]
        public NetworkRigInfo LocalRigInfo;
        public override void Start() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            singleton = this;
            int layer = LayerMask.GetMask("Player", "BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
        }
        public override void OnStartServer() {
            base.OnStartServer();
            autoCreatePlayer = false;
            _defaultSpawnPoint = SpawnPointManager.Instance.SpawnPoint.transform;
            NetworkServer.RegisterHandler<CreateRigMessage>(OnCreateRig);
        }

        public override void OnClientConnect() {
            base.OnClientConnect();

            CreateRigMessage characterMessage = new CreateRigMessage();
            characterMessage.RigInfo = LocalRigInfo;

            NetworkClient.Send(characterMessage);
        }

        private void OnCreateRig(NetworkConnectionToClient conn, CreateRigMessage message) {
            Transform spawn = DynamicSpawn ? SpawnPointManager.Instance.SpawnPoint.transform:_defaultSpawnPoint;
            var rig = Instantiate(RigPrefab, spawn.position, spawn.rotation);
            NetworkServer.AddPlayerForConnection(conn, rig.gameObject);
        }
    }
    [System.Serializable]
    public struct NetworkRigInfo {
        public string nickname;
        public Color nicknameColor;
        public string playerInfo;
        public Color playerColor;
    }
}
