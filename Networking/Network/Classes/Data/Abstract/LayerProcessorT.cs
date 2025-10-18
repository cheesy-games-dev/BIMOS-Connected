using Riptide;
using Riptide.Transports;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public abstract class LayerProcessor : MonoBehaviour {
        public static LayerProcessor singleton;
        public Server Server;
        public Client Client;
        
        protected virtual void Start() {
            Server = new Server();
            Client = new Client();
            singleton = this;
            InitSingleton();
        }

        public abstract void InitSingleton();

        public abstract void Listen(bool connectLocally);
        public abstract void Connect(string address);
        public abstract void Unlisten();
        public abstract void Disconnect();
        public abstract void Shutdown();

        public virtual void FixedUpdate() {
            Server?.Update();
            Client?.Update();
        }
    }

    public abstract class LayerProcessorT<TServer, TClient> : LayerProcessor where TServer : IServer where TClient : IClient {
        public abstract void SetupTransport(ref TServer layerServer, ref TClient layerClient);
        public ushort MaxClients = 4;
        public int MaxConnectionAttempts = 5;
        public TServer LayerServer;
        public TClient LayerClient;
        protected override void Start() {
            base.Start();
            SetupTransport(ref LayerServer, ref LayerClient);
        }
    }
}
