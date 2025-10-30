using Riptide;
using Riptide.Transports;
using Riptide.Utils;
using System;
using System.Threading;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static partial class Network
    {
        private static Thread _riptideThread;

        private static bool _isThreadAlive = false;
        public static Server Server {
            get; private set;
        }
        public static Client Client {
            get; private set;
        }
        public static Action OnServerLoadedScene;
        public static Action OnClientLoadedScene;
        static Network() {
            StartThread();
        }

        internal static void StartThread() {
            _riptideThread = new Thread(RiptideThread);

            if (_riptideThread.IsAlive)
                return;

            _isThreadAlive = true;

            _riptideThread.IsBackground = true;
            _riptideThread.Start();
        }

        private static void RiptideThread() {
            InitalizeRiptide();
            while (_isThreadAlive) {
                try {
                    Client.Update();
                    Server.Update();
                }
                catch (Exception ex) {
                    RiptideLogger.Log(Riptide.Utils.LogType.Error, $"Failed to update Riptide with exception: {ex}");
                }
            }
        }
        internal static void KillThread() {
            _isThreadAlive = false;
        }
        
        private static void InitalizeRiptide() {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);
            Message.MaxPayloadSize = 60000;

            Client = new();
            Server = new();
        }

        public static void ChangeTransport(IServer server, IClient client) {
            Server.ChangeTransport(server);
            Client.ChangeTransport(client);
        }
    }
    public enum MessageId : ushort {
        OwnershipChange = 1,
        Observable,
    }
}
