using KadenZombie8.BIMOS.Rig;
using Mirror;
using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class BIMOSNetworkManager : NetworkManager
    {
        [Header("BIMOS")]
        [SerializeField] private NETMOSRig _fallbackPlayerRig;
        public NETMOSRig FallbackPlayerPrefab {
            get => _fallbackPlayerRig;
            set => playerPrefab = value.gameObject;
        }

        public override void Start() {
            FallbackPlayerPrefab = _fallbackPlayerRig;
        }

        public override void OnStartServer() {
            base.OnStartServer();
            RegisterServerMessages();
        }

        void RegisterServerMessages() {
            NetworkServer.ReplaceHandler<GrabMessage>(GrabMessageCallback);
        }

        private void GrabMessageCallback(NetworkConnectionToClient connection, GrabMessage message) {
            if (message.IsValidGrab())
                return;
            var netId = message.Read();
            netId.RemoveClientAuthority();
            netId.AssignClientAuthority(connection);
        }
    }
}
