using FishNet.Broadcast;
using KadenZombie8.BIMOS.Rig.Movement;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    public struct GripTakeover : IBroadcast {
        public int netId;
        public GripTakeover(int netId) {
            this.netId = netId;
        }
    }
}