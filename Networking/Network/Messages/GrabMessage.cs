using System;
using FishNet.Broadcast;
using UnityEngine;

namespace KadenZombie8.BIMOS
{
    public struct GrabMessage : IBroadcast
    {
        public int GrabbableIndex;
        public float HandVelocity;

        public GrabMessage(int grabbableIndex, float handVelocity)
        {
            GrabbableIndex = grabbableIndex;
            HandVelocity = handVelocity;
        }
    }
}
