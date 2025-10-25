using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System;
using Mirror;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : NetworkBehaviour {
        private Grabbable _grip;
        public const float MAXDISTANCE = 500;
        private void Start() {
            _grip = GetComponent<Grabbable>();
            _grip.OnGrab += OnGrab;
        }
        private void OnGrab() {
            Debug.Log("[NETMOS] Sending Grab Message");
            Hand hand = _grip.RightHand ? _grip.RightHand : _grip.LeftHand;
            if(hand)SendGrabMessage(hand.transform.position);
        }

        [Command(requiresAuthority = false)]
        private void SendGrabMessage(Vector3 hand, NetworkConnectionToClient conn = null) {
            Debug.Log($"[NETMOS] Received Grab Message from {conn.address}");
            if (Vector3.Distance(transform.position, hand) > MAXDISTANCE)
                return;
            Debug.Log($"[NETMOS] Giving Ownership to {conn.address}");
            netIdentity.RemoveClientAuthority();
            netIdentity.AssignClientAuthority(conn);
        }
    }
}