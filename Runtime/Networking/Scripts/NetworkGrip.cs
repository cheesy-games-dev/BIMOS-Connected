using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System;
using Mirror;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : NetworkBehaviour, IOwnershipEvents {
        [SerializeField]
        private bool initializeUnparented = true;

        private Grabbable grabbable;
        private Rigidbody rb;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            grabbable = GetComponent<Grabbable>();

            grabbable.OnGrab += OnGrabbed;
        }

        protected override void OnValidate() {
            base.OnValidate();
            syncDirection = SyncDirection.ServerToClient;
        }

        private void OnGrabbed() {
            CmdSendTakeover();
        }

        public override void OnStartServer() {
            if (NetworkServer.active && NetworkServer.connections.Count > 0) {
                netIdentity.AssignClientAuthority(NetworkServer.connections[0]);
            }
        }

        [Command(requiresAuthority = false)]
        private void CmdSendTakeover(NetworkConnectionToClient conn = null) {
            if (connectionToClient == conn)
                return;
            netIdentity.RemoveClientAuthority();
            netIdentity.AssignClientAuthority(conn);
        }
    }
}