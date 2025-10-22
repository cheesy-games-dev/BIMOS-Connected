using Riptide;
using UnityEngine;

namespace HL.Networking {
    public static class NetworkExtensions
    {
        public static NetworkView GetNetworkView(this GameObject obj) {
            return obj.GetComponentInParent<NetworkView>();
        }
        public static NetworkView GetNetworkView(this Component obj) {
            return obj.GetComponentInParent<NetworkView>();
        }
        public static bool IsLocal(this Connection connection) => connection == Network.Client.Connection;
    }
}
