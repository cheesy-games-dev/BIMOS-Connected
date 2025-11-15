using Mirror;
using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [RequireComponent(typeof(Collider))]
    public class SyncOnCollision : NetworkBehaviour {
        [SerializeField] private bool RequireAuthority = false;
        private NetworkTransformBase m_NetworkTransform;

        private void Awake() {
            m_NetworkTransform = GetComponentInParent<NetworkTransformBase>();
        }

        private void OnCollisionEnter(Collision collision) {
            CmdTeleport(m_NetworkTransform.transform.position, m_NetworkTransform.transform.rotation);
        }

        [Command(requiresAuthority = false)]
        private void CmdTeleport(Vector3 position, Quaternion rotation, NetworkConnectionToClient conn = null) {
            if (RequireAuthority && conn != connectionToClient)
                return;
            m_NetworkTransform.RpcTeleport(position, rotation);
        }
    }
}
