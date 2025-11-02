using Mirror;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    public struct CreateRigMessage : NetworkMessage {
        public NetworkRigInfo RigInfo { get; set; }
    }
}