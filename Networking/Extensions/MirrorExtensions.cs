using Mirror;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static class MirrorExtensions
    {
        public static NetworkIdentity GetNetworkIdentity(this Component comp)
        {
            return comp.GetComponentInParent<NetworkIdentity>();
        }
        public static bool TryGetNetworkIdentity(this Component comp, out NetworkIdentity identity)
        {
            identity = comp.GetNetworkIdentity();
            return identity != null;
        }

        public static uint GetNetId(this Component comp)
        {
            return comp.GetNetworkIdentity().netId;
        }

        public static bool IsOwner(this NetworkIdentity comp)
        {
            return comp.connectionToClient == NetworkServer.localConnection;
        }

        public static bool TryGetNetId(this Component comp, out uint netid)
        {
            netid = comp.GetNetworkIdentity() != null?comp.GetNetId():uint.MinValue;
            return comp.GetNetworkIdentity() != null;
        }
    }
}
