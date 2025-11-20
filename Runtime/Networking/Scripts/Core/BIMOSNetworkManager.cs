using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

namespace KadenZombie8.BIMOS.Networking {
    public class BIMOSNetworkManager : MonoBehaviour {
        public static BIMOSNetworkManager Singleton {
            get; private set;
        }
        public Dictionary<ushort, BIMOSRig> RigCache {
            get; internal set;
        }
        public Server server;
        public Client client;
        public bool AutoHost;
        public void Awake() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            Singleton = this;
            server = new("BIMOSServer");
            client = new("BIMOSClient");
            int layer = LayerMask.GetMask("BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer);
            BIMOSRig.OnRigSpawned += RigSpawned;
        }

        private void RigSpawned(object sender, BIMOSRig e) {
            var spawnMessage = Message.Create(MessageSendMode.Reliable, ClientToServerMessages.SpawnRigRequest);
            client.Send(spawnMessage);
        }

        [MessageHandler((ushort)ClientToServerMessages.SpawnRigRequest)]
        public static void SpawnRigRequested(ushort fromClientId, Message message) {
            
        }
    }
}
