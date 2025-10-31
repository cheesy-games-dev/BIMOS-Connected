using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Connection;
using FishNet;
using FishNet.Transporting;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : MonoBehaviour {
        public static Dictionary<int, NetworkGrip> Instances { get; private set; } = new Dictionary<int, NetworkGrip>();
        public static int NextId { get; private set; } = 0;
        public List<NetworkConnection> OwnershipQueue;
        public int Id { get; private set; }
        public Grabbable grabbable;
        public NetworkObject networkObject;
        private void Awake() {
            networkObject = GetComponentInParent<NetworkObject>();
            AllocateId(this);
            grabbable = GetComponent<Grabbable>();
            grabbable.OnGrab += OnGrabbed;
        }

        public static void AllocateId(NetworkGrip networkGrip) {
            networkGrip.Id = NextId;
            Instances.Add(networkGrip.Id, networkGrip);
            NextId++;
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
            InstanceFinder.ClientManager.Broadcast(new GripTakeover(Id));
        }

        [Server]
        private void OnStartServer() {
            InstanceFinder.ServerManager.RegisterBroadcast<GripTakeover>(ServerGripTakeover);
            if (InstanceFinder.IsClientStarted) {
                networkObject.GiveOwnership(InstanceFinder.ClientManager.Connection);
            }
        }

        [Server]
        internal static void ServerGripTakeover(NetworkConnection conn, GripTakeover handler, Channel channel) {
            var grip = handler;
            var identity = Instances[grip.netId].networkObject;
            if (identity.Owner == conn)
                return;
            identity.RemoveOwnership(false);
            identity.GiveOwnership(conn);
        }
    }
}