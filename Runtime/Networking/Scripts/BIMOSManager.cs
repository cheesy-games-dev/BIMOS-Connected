using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using FishNet;

namespace KadenZombie8.BIMOS.Networking
{
    public class BIMOSManager : MonoBehaviour
    {
        [Tooltip("Prefab to spawn for the player.")]
        [SerializeField]
        private NetworkObject _networkRigVariantPrefab;


        private void Awake() {
            InitializeOnce();
        }

        private void OnDestroy() {
            InstanceFinder.SceneManager.OnClientLoadedStartScenes -= SceneManager_OnClientLoadedStartScenes;
        }

        private void InitializeOnce() {
            int layer = LayerMask.GetMask("Player");
            Physics.IgnoreLayerCollision(layer, layer, true);
            InstanceFinder.SceneManager.OnClientLoadedStartScenes += SceneManager_OnClientLoadedStartScenes;
        }

        /// <summary>
        /// Called when a client loads initial scenes after connecting.
        /// </summary>
        private void SceneManager_OnClientLoadedStartScenes(NetworkConnection conn, bool asServer) {
            if (!asServer)
                return;
            if (_networkRigVariantPrefab == null) {
                NetworkManagerExtensions.LogWarning($"Player prefab is empty and cannot be spawned for connection {conn.ClientId}.");
                return;
            }

            NetworkObject nob = InstanceFinder.NetworkManager.GetPooledInstantiated(_networkRigVariantPrefab, Vector3.zero, Quaternion.identity, true);
            InstanceFinder.ServerManager.Spawn(nob, conn);

            InstanceFinder.SceneManager.AddOwnerToDefaultScene(nob);
        }
    }
}
