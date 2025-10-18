using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static class NetworkExtensions
    {
        public static NetworkEntityHost GetNetworkEntity(this Component component) {
            return component.GetComponentInParent<NetworkEntityHost>();
        }
        public static NetworkEntityHost GetNetworkEntity(this GameObject gameObject) {
            return gameObject.GetComponentInParent<NetworkEntityHost>();
        }
        public static bool TryGetNetworkEntity(this GameObject gameObject, out NetworkEntityHost entity) {
            entity = gameObject.GetNetworkEntity();
            return entity;
        }
        public static bool TryGetNetworkEntity(this Component component, out NetworkEntityHost entity) {
            entity = component.GetNetworkEntity();
            return entity;
        }
    }
}
