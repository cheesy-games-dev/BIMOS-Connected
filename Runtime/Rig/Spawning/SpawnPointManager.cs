using KadenZombie8.BIMOS.Networking;
using Mirror;
using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Rig.Spawning
{
    /// <summary>
    /// Manages the current spawn point and respawning the player
    /// </summary>
    [DefaultExecutionOrder(-2)]
    public class SpawnPointManager : MonoBehaviour
    {
        public static SpawnPointManager Instance { get; private set; }
        public event Action OnRespawn;

        public BIMOSRig RigPrefab;

        public SpawnPoint CurrentSpawnPoint;

        private void OnEnable() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            if (!CurrentSpawnPoint) {
                CurrentSpawnPoint = FindFirstObjectByType<SpawnPoint>();
                if (!CurrentSpawnPoint) {
                    Debug.LogError("You must have at least one spawn point!");
                    return;
                }
            }
            Instance = this;
            NetworkManager.singleton.playerPrefab = RigPrefab.gameObject;
            NetworkManager.RegisterStartPosition(CurrentSpawnPoint.transform);
        }

        private void OnDisable() {
            NetworkManager.UnRegisterStartPosition(CurrentSpawnPoint.transform);
        }

        private void Start() => Respawn();

        public void SetSpawnPoint(SpawnPoint spawnPoint) {
            NetworkManager.UnRegisterStartPosition(CurrentSpawnPoint.transform);
            CurrentSpawnPoint = spawnPoint;
            NetworkManager.RegisterStartPosition(CurrentSpawnPoint.transform);
        }

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