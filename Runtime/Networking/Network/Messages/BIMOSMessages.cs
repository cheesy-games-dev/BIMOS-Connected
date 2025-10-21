using KadenZombie8.BIMOS.Rig;
using Mirror;
using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public struct GrabMessage : NetworkMessage
    {
        public uint NetId { get; private set; }
        public float Distance {
            get; private set;
        }
        public float Bounds;
        public GrabMessage(NetworkIdentity netId, Grabbable grabbable, Hand hand) {
            NetId = netId.netId;
            Distance = Vector3.Distance(grabbable.transform.position, hand.PhysicsHandTransform.position);
            Bounds = grabbable.Collider.bounds.size.sqrMagnitude;
        }
        public NetworkIdentity Read() => NetworkClient.spawned[NetId];
        public bool IsValidGrab() => Distance > Bounds;
    }
}
