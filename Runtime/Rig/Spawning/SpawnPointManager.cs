using FishNet;
using FishNet.Connection;
using KadenZombie8.BIMOS.Networking;
using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Rig.Spawning
{
    /// <summary>
    /// Manages the current spawn point and respawning the player
    /// </summary>
    public class SpawnPointManager : MonoBehaviour
    {
        public static SpawnPointManager Instance { get; private set; }
        public event Action OnRespawn;

        public BIMOSRig RigPrefab;

        public SpawnPoint CurrentSpawnPoint;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            if (!CurrentSpawnPoint)
            {
                CurrentSpawnPoint = FindFirstObjectByType<SpawnPoint>();
                if (!CurrentSpawnPoint)
                {
                    Debug.LogError("You must have at least one spawn point!");
                    return;
                }
            }
        }

        private void OnEnable() {
            InstanceFinder.SceneManager.OnClientLoadedStartScenes += SceneManager_OnClientLoadedStartScenes;
        }

        private void OnDisable() {
            InstanceFinder.SceneManager.OnClientLoadedStartScenes -= SceneManager_OnClientLoadedStartScenes;
        }

        private void SceneManager_OnClientLoadedStartScenes(NetworkConnection arg1, bool arg2) {
            var rigInstance = Instantiate(RigPrefab);
            InstanceFinder.ServerManager.Spawn(rigInstance.GetNetworkObject(), arg1);
            TeleportToSpawnPoint(CurrentSpawnPoint.transform, rigInstance);
        }

        private void Start() => Respawn();

        public void SetSpawnPoint(SpawnPoint spawnPoint) => CurrentSpawnPoint = spawnPoint;

        public void Respawn()
        {
            TeleportToSpawnPoint(CurrentSpawnPoint.transform);

            OnRespawn?.Invoke();
        }

        private void TeleportToSpawnPoint(Transform spawnPoint, BIMOSRig rig = null) {
            rig ??= BIMOSRig.Instance;
            rig.PhysicsRig.GrabHandlers.Left.AttemptRelease();
            rig.PhysicsRig.GrabHandlers.Right.AttemptRelease();

            var rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
            var rootPosition = rig.PhysicsRig.Rigidbodies.LocomotionSphere.position;
            foreach (var rigidbody in rigidbodies) {
                var offset = rigidbody.position - rootPosition; //Calculates the offset between the locoball and the rigidbody
                rigidbody.position = spawnPoint.position + offset; //Sets the rigidbody's position
                rigidbody.transform.position = spawnPoint.position + offset; //Sets the transform's position

                if (rigidbody.isKinematic)
                    continue;

                rigidbody.linearVelocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }

            //Update the animation rig's position
            rig.AnimationRig.Transforms.Hips.position += spawnPoint.position - rootPosition;

            //Move the player's animated feet to the new position
            rig.AnimationRig.Feet.TeleportFeet();

            rig.ControllerRig.transform.rotation = transform.rotation;
        }
    }
}