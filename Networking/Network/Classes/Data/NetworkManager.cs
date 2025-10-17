using UnityEngine;
using System.Collections.Generic;
using KadenZombie8.BIMOS.Rig;
using Riptide.Utils;
namespace KadenZombie8.BIMOS.Networking
{
    [RequireComponent(typeof(SceneProcessor), typeof(LayerProcessor))]
    public class NetworkManager : MonoBehaviour
    {
        public NetworkBarcodeT<BIMOSRig> RigPrefab = new NetworkBarcodeT<BIMOSRig>();
        public List<NetworkBarcodeT<SceneReference>> SceneList = new();
        public List<NetworkBarcodeT<GameObject>> PrefabList = new();
        public static NetworkManager singleton {
            get; set;
        }

        public string ServerCode;

        protected virtual void Awake() {
            if (singleton && singleton != this)
                return;
            singleton = this;
        }

        protected virtual void Start() {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        }

        public virtual void CreateServer(bool host = true) {
            LayerProcessor.singleton.CreateServer(host);
        }

        public virtual void JoinServer(string code = "localhost") {
            if (ServerCode != null)
                code = ServerCode;
            LayerProcessor.singleton.JoinServer(code);
        }

        public virtual void ServerChangeScene(NetworkBarcodeT<SceneReference> scene) {
            SceneProcessor.singleton.LoadScene(scene);
        }

        public virtual void ServerUnloadScene(NetworkBarcodeT<SceneReference> scene) {
            SceneProcessor.singleton.LoadScene(scene);
        }
    }
}
