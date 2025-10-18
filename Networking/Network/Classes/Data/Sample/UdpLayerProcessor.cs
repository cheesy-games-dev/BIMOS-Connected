using Riptide.Transports.Udp;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class UdpLayerProcessor : LayerProcessorT<UdpServer, UdpClient>
    {
        public ushort Port = 7777;
        public override void Listen(bool connectLocally) {
            Server.Start(Port, MaxClients);
            if (connectLocally)
                Connect("localhost");
        }

        public new static UdpLayerProcessor singleton;
        public override void InitSingleton() {
            singleton = this;
        }

        public override void Connect(string address) {
            Client.Connect(address, MaxConnectionAttempts);
        }

        public override void SetupTransport(ref UdpServer layerServer, ref UdpClient layerClient) {
            layerServer = new();
            Server?.ChangeTransport(layerServer);
            layerClient = new();
            Client?.ChangeTransport(layerClient);
        }

        public override void Disconnect() {
            Client?.Disconnect();
        }

        public override void Unlisten() {
            Server?.Stop();
        }

        public override void Shutdown() {
            Disconnect();
            Unlisten();
        }
    }
}
