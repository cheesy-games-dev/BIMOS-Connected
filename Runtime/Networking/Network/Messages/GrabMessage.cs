using Mirror;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public struct GrabMessage : NetworkMessage
    {
        public uint NetId {get; set;}
        public GrabMessage(NetworkIdentity netId) {
            NetId = netId.netId;
        }
        public NetworkIdentity Read() => NetworkClient.spawned[NetId];
    }
}
