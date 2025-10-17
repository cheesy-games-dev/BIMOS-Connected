using Riptide;
using Riptide.Transports;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public abstract class LayerProcessor : MonoBehaviour {
        public static LayerProcessor singleton {
            get; set;
        }
        public Server Server {
            get; set;
        }
        public Client Client {
            get; set;
        }
        public IServer ServerTransport 
            { get; set; }
        public IClient ClientTransport 
            { get; set; }
        public ushort MaxClients = 4;
        public int MaxConnectionAttempts = 5;
        private void Start() {
            if (singleton && singleton != this)
                return;
            singleton = this;

            Server = new Server();
            Client = new Client();
        }

        public abstract void CreateServer(bool host);

        public abstract void JoinServer(string code);

        public virtual void FixedUpdate() {
            Server?.Update();
            Client?.Update();
        }
    }
}
