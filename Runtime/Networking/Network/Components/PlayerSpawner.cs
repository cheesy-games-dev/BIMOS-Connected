using Riptide;
using UnityEngine;

namespace HL.Networking
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject PlayerPrefab;
        private void Start() {
            Network.Server.ClientConnected += Client_ClientConnected;
        }

        private void Client_ClientConnected(object sender, ServerConnectedEventArgs e) {
            if (e != null && e.Client != null)
                Network.Instantiate(PlayerPrefab, transform.position, transform.rotation, e.Client);
        }
    }
}
