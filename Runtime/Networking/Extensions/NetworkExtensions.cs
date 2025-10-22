using Mirror;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static class NetworkExtensions
    {
        public static NetworkIdentity GetNetworkIdentity(this Component component) => component.GetComponentInParent<NetworkIdentity>();

        public static NetworkIdentity GetNetworkIdentity(this GameObject gameObject) => gameObject.GetComponentInParent<NetworkIdentity>();

        public static bool IsOwner(this Component component) => component.GetNetworkIdentity().connectionToClient == NetworkServer.localConnection;
    }
}
