using Riptide.Utils;
using UnityEngine;

namespace HL.Networking {
    public class NetworkGUI : MonoBehaviour
    {
        private string address = string.Empty;
        private ushort port = 7777;
        private ushort maxclients = 32;
        public bool DebugMode = true;
        private void Start() {
            if(DebugMode) RiptideLogger.EnableLoggingFor(Riptide.Utils.LogType.Debug, Debug.Log);
            else
                RiptideLogger.EnableLoggingFor(Riptide.Utils.LogType.Debug, Debug.Log);
        }
        private void OnGUI() {
            GUILayout.Box("Network Status");
            GUILayout.Label($"Server Running: {Network.Server.IsRunning}");
            GUILayout.Label($"Client Connected: {Network.Client.IsConnected}");
            GUILayout.Box("Network Manager");
            GUILayout.Label("port");
            port = ushort.Parse(GUILayout.TextField($"{port}"));
            GUILayout.Label("max clients");
            maxclients = ushort.Parse(GUILayout.TextField($"{maxclients}"));
            if (GUILayout.Button("Start Server"))
                Network.InitializeServer(maxclients, port);
            if (GUILayout.Button("Start Host"))
                Network.InitializeHost(maxclients, port);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Address");
            address = GUILayout.TextField(address);
            if (GUILayout.Button("Connect"))
                Network.Connect(address, port);
            GUILayout.EndHorizontal();
        }
    }
}
