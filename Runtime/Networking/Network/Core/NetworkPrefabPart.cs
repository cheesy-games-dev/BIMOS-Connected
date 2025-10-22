using Riptide;
using Riptide.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HL.Networking
{
    public static partial class Network {
        public enum ServerToClientId : ushort {
            instantiate = 1,
            allocate,
            destroy,
        }
        public static Dictionary<ushort, NetworkView> Views = new Dictionary<ushort, NetworkView>();
        public static void AllocateViewID(NetworkView view) {
            while (!Views.ContainsKey(view.Id)) {
                view.Id++;
            }
            Views.Add(view.Id, view);
            var message = Message.Create(MessageSendMode.Reliable, ServerToClientId.allocate);
            message.Add(view.Id);
            message.Add(view.transform.position);
            message.Add(view.transform.rotation);
            message.Add(view.Owner);
        }
       
        public static GameObject Instantiate(GameObject prefab, Vector3 position = new(), Quaternion rotation = new(), Connection owner = null) {
            Debug.Log("Attempting to Instantiate Object");
            if (!Server.IsRunning) {
                Debug.LogError($"Server not Active");
            }
            owner ??= Server.Clients[0];
            var prefabKey = Settings.Prefabs.Find(x => x.value == prefab).key;
            Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.instantiate);
            message.Add(prefabKey);
            Server.SendToAll(message);

            var instance = Object.Instantiate(prefab, position, rotation);
            instance.GetNetworkView().Owner = owner.Id;
            
            AllocateViewID(instance.GetNetworkView());
            return instance;
        }

        internal static GameObject _Instantiate(string prefabKey) {
            var instance = Object.Instantiate(Settings.Prefabs.Find(x => x.key == prefabKey).value);
            return instance;
        }

        [MessageHandler((ushort)ServerToClientId.allocate)]
        internal static void _AllocateViewClient(Message message) {
            var view = Views[message.GetUShort()];
            view.transform.position = message.GetVector3();
            view.transform.rotation = message.GetQuaternion();
            view.Owner = message.GetUShort();
        }

        [MessageHandler((ushort)ServerToClientId.instantiate)]
        internal static void _InstantiateClient(Message message) {
            _Instantiate(message.GetString());
        }
    }
}
