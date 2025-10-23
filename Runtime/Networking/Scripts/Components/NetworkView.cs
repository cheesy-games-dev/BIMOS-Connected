using UnityEngine;
using Mirror;
namespace KadenZombie8.BIMOS.Networking
{
    public class NetworkView : NetworkBehaviour
    {
        public override void OnStopServer() {
            base.OnStopServer();
        }
        public override void OnStopClient() {
            base.OnStopClient();
        }
    }
}
