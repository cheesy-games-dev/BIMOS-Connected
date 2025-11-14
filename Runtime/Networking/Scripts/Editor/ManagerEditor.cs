#if UNITY_EDITOR
using FishNet;
using UnityEditor;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [CustomEditor(typeof(Manager))]
    public class ManagerEditor : UnityEditor.Editor
    {
        string address = string.Empty;
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUI.BeginDisabledGroup(!Application.IsPlaying(target));
            if (InstanceFinder.IsOffline) {
                if (GUILayout.Button("Start Host")) {
                    InstanceFinder.ServerManager.StartConnection();
                    InstanceFinder.ClientManager.StartConnection();
                }
                if (GUILayout.Button("Start Server")) {
                    InstanceFinder.ServerManager.StartConnection();
                }
                address = EditorGUILayout.TextField("Address", address);
                if (GUILayout.Button("Start Client")) {
                    InstanceFinder.ClientManager.StartConnection(address);
                }
            }
            else {
                if (GUILayout.Button("Shutdown")) {
                    InstanceFinder.ClientManager.StopConnection();
                    InstanceFinder.ServerManager.StopConnection(true);
                }
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
#endif