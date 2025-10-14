using System;
using Mirror;
using UnityEngine;

namespace KadenZombie8.BIMOS
{
    public struct GrabMessage : NetworkMessage
    {
        public NetworkIdentity GrabbableId;
        public float HandVelocity;

        public GrabMessage(NetworkIdentity grabbableId, float handVelocity)
        {
            GrabbableId = grabbableId;
            HandVelocity = handVelocity;
        }
    }
}
