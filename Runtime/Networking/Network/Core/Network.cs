using Riptide;
using Riptide.Transports;
using Riptide.Utils;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace HL.Networking
{
    public static partial class Network {
        public static Server Server {
            get; internal set;
        }
        public static Client Client {
            get; internal set;
        }
        public static NetworkSettings Settings {
            get; internal set;
        }
        static Network() {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
            var handler = new GameObject("NetworkHandler", typeof(NetworkHandler));
            Object.DontDestroyOnLoad(handler);
        }

        public static void ChangeTransport(IServer server, IClient client) {
            Server?.ChangeTransport(server);
            Client?.ChangeTransport(client);
        }
        public static void InitializeHost(ushort maxClients, ushort port) {
            InitializeServer(maxClients, port);
            Connect("127.0.0.1", port);
        }
        public static void InitializeServer(ushort maxClients, ushort port) {
            Server?.Start(port, maxClients);
        }
        public static void Connect(string address, ushort port) {
            Client?.Connect($"{address}:{port}");
        }
        public static void Disconnect() {
            Server?.Stop();
            Client?.Disconnect();
        }
    }  
}
