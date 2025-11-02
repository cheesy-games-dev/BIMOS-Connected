using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;
using Mirror;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : OwnershipAutoAssigner {
        public static Dictionary<int, NetworkGrip> Instances { get; private set; } = new Dictionary<int, NetworkGrip>();
        public static int NextId { get; private set; } = 0;
        public List<NetworkConnectionToClient> OwnershipQueue { get; private set; } = new();
        public Grabbable grabbable;
        private void Awake() {
            grabbable = GetComponent<Grabbable>();
            grabbable.OnGrab += OnGrabbed;
            grabbable.OnRelease += OnReleased;
        }
        [Client]
        private void OnGrabbed() {
            SendGripTakeoverCmd();
        }
        [Client]
        private void OnReleased() {
            SendGripReleaseCmd();
        }

        [Command(requiresAuthority = false)]
        private void SendGripTakeoverCmd(NetworkConnectionToClient conn = null) {
            if (!OwnershipQueue.Contains(conn))
                OwnershipQueue.Add(conn);
            AssignOwnership();
        }
        [Command(requiresAuthority = false)]
        private void SendGripReleaseCmd(NetworkConnectionToClient conn = null) {
            if(OwnershipQueue.Contains(conn)) OwnershipQueue.Remove(conn);
            AssignOwnership();
        }

        [Server]
        private void AssignOwnership() {
            if (OwnershipQueue.Count <= 0 || connectionToClient == OwnershipQueue[0])
                return;
            netIdentity.RemoveClientAuthority();
            netIdentity.AssignClientAuthority(OwnershipQueue[0]);
        }
    }
}