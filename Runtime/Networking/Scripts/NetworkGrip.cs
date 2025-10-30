using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;
using Riptide;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : MonoBehaviourNetwork {
        private static Dictionary<int, NetworkGrip> gripInstances = new Dictionary<int, NetworkGrip>();
        public int id;
        private Grabbable grabbable;

        private void Awake() {
            id = gripInstances.AllocateId(this);
            grabbable = GetComponent<Grabbable>();
            grabbable.OnGrab += OnGrabbed;
        }

        private void Start() {
            OnStartServer();
        }

        [ClientCallback]
        private void OnGrabbed() {
            SendGripTakeover();
        }

        [ClientCallback]
        private void SendGripTakeover() {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)ServerMessages.GripTakeover);
            message.Add(new GripTakeover(id));
            Network.Client.Send(message);
        }

        [ServerCallback]
        private void OnStartServer() {
            if (Network.Client.IsConnected) {
                View.GiveOwnership(Network.Client.Connection.Id);
            }
        }

        [ServerCallback, MessageHandler((ushort)ServerMessages.GripTakeover)]
        internal static void ServerGripTakeover(Connection conn, Message message) {
            var grip = message.GetSerializable<GripTakeover>();
            var networkObject = gripInstances[grip.netId].View;
            if (networkObject.Owner == conn.Id)
                return;
            networkObject.RemoveOwnership();
            networkObject.GiveOwnership(conn.Id);
            ;
        }
    }
}