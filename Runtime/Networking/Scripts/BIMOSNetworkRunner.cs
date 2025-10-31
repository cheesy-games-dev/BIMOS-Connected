using Fusion;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [RequireComponent(typeof(NetworkRunner))]
    public class BIMOSNetworkRunner : MonoBehaviour
    {
        public static BIMOSNetworkRunner Instance { get; private set; }
        public NetworkRunner runner {
            get; private set;
        }
        public bool IsHost => runner.IsSharedModeMasterClient || runner.IsServer;
        public bool IsClient => runner.IsClient;
        private void Start() {
            if (Instance && Instance != this) {
                Destroy(gameObject);
                return;
            }
            runner = GetComponent<NetworkRunner>();
            Instance = this;
            DontDestroyOnLoad(runner);
            runner.name = "Network Runner";
        }
    }
}
