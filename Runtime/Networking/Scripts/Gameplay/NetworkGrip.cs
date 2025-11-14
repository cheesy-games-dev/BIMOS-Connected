using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Connection;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : OwnershipAutoAssigner {
        public static Dictionary<int, NetworkGrip> Instances { get; private set; } = new Dictionary<int, NetworkGrip>();
        public static int NextId { get; private set; } = 0;
        public List<NetworkConnection> OwnershipQueue { get; private set; } = new();
        public Grabbable grabbable;
        private void Awake() {
            grabbable = GetComponent<Grabbable>();
            grabbable.OnGrab.AddListener(OnGrabbed);
            grabbable.OnRelease.AddListener(OnReleased);
        }
        [Client]
        private void OnGrabbed() {
            SendGripTakeoverCmd();
        }
        [Client]
        private void OnReleased() {
            SendGripReleaseCmd();
        }

        [ServerRpc(RequireOwnership = false)]
        private void SendGripTakeoverCmd(NetworkConnection conn = null) {
            if (!OwnershipQueue.Contains(conn))
                OwnershipQueue.Add(conn);
            AssignOwnership();
        }
        [ServerRpc(RequireOwnership = false)]
        private void SendGripReleaseCmd(NetworkConnection conn = null) {
            if(OwnershipQueue.Contains(conn)) OwnershipQueue.Remove(conn);
            AssignOwnership();
        }

        [Server]
        private void AssignOwnership() {
            if (OwnershipQueue.Count <= 0 || Owner == OwnershipQueue[0])
                return;
            NetworkObject.RemoveOwnership();
            NetworkObject.GiveOwnership(OwnershipQueue[0]);
        }
    }
}