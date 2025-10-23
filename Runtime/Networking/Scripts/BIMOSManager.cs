using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using FishNet;
using KadenZombie8.BIMOS.Rig.Spawning;
using System;

namespace KadenZombie8.BIMOS.Networking
{
    public class BIMOSManager : MonoBehaviour
    {
        [Tooltip("Prefab to spawn for the player.")]
        [SerializeField]
        public NetworkObject networkRigPrefab;


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
            if (networkRigPrefab == null) {
                NetworkManagerExtensions.LogWarning($"Player prefab is empty and cannot be spawned for connection {conn.ClientId}.");
                return;
            }
            GetCurrentSpawnPointCords(out Vector3 pos, out Quaternion rot);
            NetworkObject nob = InstanceFinder.NetworkManager.GetPooledInstantiated(networkRigPrefab, pos, rot, true);
            InstanceFinder.ServerManager.Spawn(nob, conn);

            InstanceFinder.SceneManager.AddOwnerToDefaultScene(nob);
        }

        private void GetCurrentSpawnPointCords(out Vector3 pos, out Quaternion rot) {
            pos = SpawnPointManager.Instance.SpawnPoint.transform.position;
            rot = SpawnPointManager.Instance.SpawnPoint.transform.rotation;
        }
    }
}
