using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : MonoBehaviourNetwork {
        private static Dictionary<int, NetworkGrip> gripInstances = new Dictionary<int, NetworkGrip>();
        public int id;
        private Grabbable grabbable;
        private Rigidbody rb;

        private void Awake() {
            id = gripInstances.AllocateId(this);
            rb = GetComponent<Rigidbody>();
            grabbable = GetComponent<Grabbable>();
            grabbable.OnGrab += OnGrabbed;
        }

        private void Start() {
            OnStartServer();
        }

        [Client]
        private void OnGrabbed() {
            SendGripTakeover();
        }

        [Client]
        private void SendGripTakeover() {
            InstanceFinder.ClientManager.Broadcast(new GripTakeover(id));
        }

        [Server]
        private void OnStartServer() {
            InstanceFinder.ServerManager.RegisterBroadcast<GripTakeover>(ServerGripTakeover);
            if (networkObject.LocalConnection != null) {
                networkObject.GiveOwnership(networkObject.LocalConnection);
            }
        }

        [Server]
        private static void ServerGripTakeover(NetworkConnection conn, GripTakeover grip, Channel channel) {
            if(channel != Channel.Reliable)
                return;
            var networkObject = gripInstances[grip.netId].networkObject;
            if (networkObject.Owner == conn)
                return;
            networkObject.RemoveOwnership(false);
            networkObject.GiveOwnership(conn);
        }
    }

    public struct GripTakeover : IBroadcast {
        public int netId;
        public GripTakeover(int netId) {
            this.netId = netId;
        }
    }
}