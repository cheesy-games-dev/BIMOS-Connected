using UnityEngine;
using System.Collections.Generic;
using KadenZombie8.BIMOS.Rig;
using Riptide.Utils;
using Riptide.Transports;
namespace KadenZombie8.BIMOS.Networking
{
    public class NetworkManager : MonoBehaviour
    {
        public NetworkBarcodeT<BIMOSRig> RigPrefab = new NetworkBarcodeT<BIMOSRig>();
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

        public virtual void CreateServer(bool hostMode) {
            LayerProcessorT<IServer, IClient>.singleton.Listen(hostMode);
        }

        public virtual void JoinServer(string address) {
            LayerProcessorT<IServer, IClient>.singleton.Connect(address);
        }

        public virtual void ServerChangeScene(bool hostMode) {
            SceneProcessor<object>.singleton.LoadSceneAsync(hostMode);
        }

        public virtual void JoinServer(string address) {
            LayerProcessorT<IServer, IClient>.singleton.Connect(address);
        }
    }
}
