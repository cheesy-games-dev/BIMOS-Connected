using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using System;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : NetworkBehaviour {
        public static Dictionary<int, NetworkGrip> Instances { get; private set; } = new Dictionary<int, NetworkGrip>();
        public static int NextId { get; private set; } = 0;
        public List<NetworkConnection> OwnershipQueue { get; private set; } = new();
        public Grabbable grabbable;
        private void Awake() {
            grabbable = GetComponent<Grabbable>();
            grabbable.OnGrab += OnGrabbed;
            grabbable.OnRelease += OnReleased;
        }
        [Client]
        private void OnGrabbed() {
            SendGripTakeoverRpc();
        }
        [Client]
        private void OnReleased() {
            SendGripReleaseRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void SendGripTakeoverRpc(NetworkConnection conn = null) {
            OwnershipQueue.Add(conn);
            AssignOwnership();
        }
        [ServerRpc(RequireOwnership = false)]
        private void SendGripReleaseRpc(NetworkConnection conn = null) {
            OwnershipQueue.Remove(conn);
            AssignOwnership();
        }
       
        private void AssignOwnership() {
            if (Owner == OwnershipQueue[0] || OwnershipQueue.Count <= 0)
                return;
            RemoveOwnership();
            GiveOwnership(OwnershipQueue[0]);
        }
    }
}