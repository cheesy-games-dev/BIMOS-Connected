using UnityEngine;

namespace HL.Networking
{
    public class NetworkHandler : MonoBehaviour {
        private static NetworkHandler _singleton;
        public static NetworkHandler Singleton {
            get => _singleton;
            private set {
                if (_singleton == null)
                    _singleton = value;
                else if (_singleton != value) {
                    Debug.Log($"{nameof(NetworkHandler)} instance already exists, destroying duplicate!");
                    Destroy(value);
                }
            }
        }
        private void Awake() {
            if (!Application.isPlaying) {
                Destroy(gameObject);
                return;
            }
            Singleton = this;
            Network.Server = new();
            Network.Client = new();
        }
        private void Update() {
            Network.Server?.Update();
            Network.Client?.Update();
        }
    }
}
