using UnityEngine;
using System.Collections.Generic;
using KadenZombie8.BIMOS.Rig;
using KadenZombie8.BIMOS.Rig.Movement;
using Riptide;

namespace KadenZombie8.BIMOS.Networking {
    public class BIMOSManager : MonoBehaviour {
        public static BIMOSManager singleton;
        public Dictionary<int, BIMOSRig> BIMOSRigs;
        public BIMOSRig BIMOSRigPrefab;
        private void Awake() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            singleton = this;
            int layer = LayerMask.GetMask("Player", "BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
        }

        [ClientCallback, MessageHandler((ushort)ClientMessages.PlayerSynced)]
        private void SyncPlayerClientCallback(SyncPlayer player) {
            var rig = BIMOSRigs[player.clientId];
            player.SetPhysicsRig(rig.PhysicsRig);
        }
        [ServerCallback, MessageHandler((ushort)ServerMessages.SyncPlayer)]
        private void SyncPlayerServerCallback(Connection connection, Message client) {
            var player = client.GetSerializable<SyncPlayer>();
            player.clientId = connection.Id;
            Message message = Message.Create(MessageSendMode.Unreliable, (ushort)ClientMessages.PlayerSynced);
            message.Add(player);
            Network.Server.SendToAll(message, connection.Id);
        }
        [ClientCallback, MessageHandler((ushort)ClientMessages.PlayerSpawned)]
        private void SpawnPlayerCallback(SpawnPlayer player) {
            var rig = Instantiate(BIMOSRigPrefab);
            rig.enabled = false;
            rig.ControllerRig.gameObject.SetActive(false);
            rig.PhysicsRig.enabled = false;
            BIMOSRigs.Add(player.clientId, rig);
        }

        private void Update() {
            SyncPlayer player = new(BIMOSRig.Instance.PhysicsRig, -1);
            Message message = Message.Create(MessageSendMode.Unreliable, (ushort)ServerMessages.SyncPlayer);
            message.Add(player);
            Network.Client.Send(message);
        }
        [ServerCallback, MessageHandler((ushort)ServerMessages.SpawnPlayer)]
        private void SceneManager_OnClientLoadedStartScenes(Connection conn) {
            SpawnPlayer player = new SpawnPlayer {
                clientId = conn.Id
            };
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientMessages.PlayerSpawned);
            message.Add(player);
            Network.Server.SendToAll(message, conn.Id);
        }
    }
}
