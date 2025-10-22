using KadenZombie8.BIMOS.Rig;
using Riptide;
using Riptide.Transports;
using Riptide.Utils;
using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public partial class Network : MonoBehaviour
    {
        private static Network _singleton;
        public static Network Singleton {
            get => _singleton;
            private set {
                if (_singleton == null)
                    _singleton = value;
                else if (_singleton != value) {
                    Debug.Log($"{nameof(Network)} instance already exists, destroying duplicate!");
                    Destroy(value);
                }
            }
        }

        public Server Server {
            get; private set;
        }
        public Client Client {
            get; private set;
        }

        public ushort MaxClientCount;

        private void Awake() {
            Singleton = this;
        }

        private void Start() {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

            Server = new();
            Client = new();
        }
        public void ChangeTransport(IServer server, IClient client) {
            Server?.ChangeTransport(server);
            Client?.ChangeTransport(client);
        }
        public void StartHost() {
            Listen();
            Connect();
        }
        public void Listen(ushort port = 7777) {
            Server?.Start(port, MaxClientCount);
        }
        public void Connect(string address = "localhost") {
            Client?.Connect(address);
        }

        private void FixedUpdate() {
            Server?.Update();
            Client?.Update();
        }

        private void OnApplicationQuit() {
            Server.Stop();
            Client?.Disconnect();
        }
    }
}
