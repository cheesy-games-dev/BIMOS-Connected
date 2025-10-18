using Riptide.Transports;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class NetworkManagerHUD : MonoBehaviour
    {
        string servercode = "localhost";
        private void OnGUI() {
            GUILayout.Box("BIMOS Connected");
            if (GUILayout.Button("Start Host"))
                LayerProcessorT<IServer, IClient>.singleton.Listen(true);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Start Server"))
                LayerProcessorT<IServer, IClient>.singleton.Listen(true);
            if (GUILayout.Button("Start Client"))
                LayerProcessorT<IServer, IClient>.singleton.Connect(servercode);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.TextField("Server Code");
            GUILayout.TextField(servercode);
            GUILayout.BeginHorizontal();
            GUILayout.Button("Shutdown");
        }
    }
}
