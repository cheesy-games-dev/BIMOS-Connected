using Riptide.Transports.Udp;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class UdpLayerProcessor : LayerProcessor
    {
        public ushort Port = 7777;
        public override void CreateServer(bool host) {
            ServerTransport = new UdpServer();
            Server.ChangeTransport(ServerTransport);
            Server.Start(Port, MaxClients);
        }

        public override void JoinServer(string code) {
            ClientTransport = new UdpClient();
            Client.ChangeTransport(ClientTransport);
            Client.Connect(code, MaxConnectionAttempts);
        }
    }
}
