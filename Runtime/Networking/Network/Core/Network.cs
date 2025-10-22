using KadenZombie8.BIMOS.Rig;
using Riptide;
using Riptide.Transports;
using Riptide.Utils;
using System;
using System.Threading;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static partial class Network {
        public static Server Server {
            get; private set;
        }
        public static Client Client {
            get; private set;
        }
        private static Thread _riptideThread;
        static Network() {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

            Server = new();
            Client = new();
            StartThread();
        }
        internal static void StartThread() {
            _riptideThread = new Thread(RiptideThread);

            if (_riptideThread.IsAlive)
                return;

            _riptideThread.IsBackground = true;
            _riptideThread.Start();
        }
        public static void ChangeTransport(IServer server, IClient client) {
            Server?.ChangeTransport(server);
            Client?.ChangeTransport(client);
        }
        public static void InitializeHost(ushort maxClients, ushort port) {
            InitializeServer(maxClients, port);
            Connect("127.0.0.1", 25000);
        }
        public static void InitializeServer(ushort maxClients, ushort port) {
            Server?.Start(port, maxClients);
        }
        public static void Connect(string address, ushort port) {
            Client?.Connect(address);
        }
        public static void Disconnect() {
            Server?.Stop();
            Client?.Disconnect();
        }

        private static void RiptideThread() {
            Server?.Update();
            Client?.Update();
        }
    }
}
