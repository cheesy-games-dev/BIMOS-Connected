using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static class NetworkExtensions
    {
        public static GameObjectBarcode GetNetworkEntity(this Component component) {
            return component.GetComponentInParent<GameObjectBarcode>();
        }

        public static bool TryGetNetworkEntity(this Component component, out GameObjectBarcode entity) {
            entity = component.GetNetworkEntity();
            return entity;
        }
    }
}
