using FishNet.Object;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static class NetworkExtensions
    {
        public static NetworkObject GetNetworkObject(this Component component) => component.GetComponentInParent<NetworkObject>();

        public static NetworkObject GetNetworkObject(this GameObject gameObject) => gameObject.GetComponentInParent<NetworkObject>();
    }
}
