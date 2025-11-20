#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using FishNet;
using FishNet.Managing.Transporting;

[CustomEditor(typeof(TransportManager), true), CanEditMultipleObjects]
public class TransportManagerEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Start Host")) {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        }
        if (GUILayout.Button("Start Server")) {
            InstanceFinder.ServerManager.StartConnection();
        }
        if (GUILayout.Button("Start Client")) {
            InstanceFinder.ClientManager.StartConnection();
        }
    }
}
#endif