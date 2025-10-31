using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;
using Fusion;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : NetworkBehaviour {
        public static Dictionary<int, NetworkGrip> Instances { get; private set; } = new Dictionary<int, NetworkGrip>();
        public static int NextId { get; private set; } = 0;
        public List<PlayerRef> OwnershipQueue;
        public Grabbable grabbable;
        private void Awake() {
            grabbable = GetComponent<Grabbable>();
            grabbable.OnGrab += OnGrabbed;
        }

        private void OnGrabbed() {
            SendGripTakeoverRpc(Runner.LocalPlayer);
            Object.RequestStateAuthority();
        }

        [Rpc(RpcSources.Proxies, RpcTargets.StateAuthority)]
        private void SendGripTakeoverRpc(PlayerRef player) {
            Object.ReleaseStateAuthority();
        }
    }
}