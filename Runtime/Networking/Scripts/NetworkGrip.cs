using UnityEngine;
using FishNet.Object;
using KadenZombie8.BIMOS.Rig;
using System;
using FishNet.Connection;

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

        [ServerRpc(RequireOwnership = false)]
        private void SendGrabMessage(Vector3 hand, NetworkConnection conn = null) {
            Debug.Log($"[NETMOS] Received Grab Message from {conn.GetAddress()}");
            if (Vector3.Distance(transform.position, hand) > MAXDISTANCE)
                return;
            Debug.Log($"[NETMOS] Giving Ownership to {conn.GetAddress()}");
            NetworkObject.RemoveOwnership(false);
            NetworkObject.GiveOwnership(conn);
        }
    }
}