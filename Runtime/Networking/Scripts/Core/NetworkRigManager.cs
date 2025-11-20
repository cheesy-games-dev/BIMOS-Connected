using FishNet;
using FishNet.Broadcast;
using FishNet.Connection;
using FishNet.Managing.Client;
using FishNet.Managing.Server;
using FishNet.Transporting;
using KadenZombie8.BIMOS.Rig;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KadenZombie8.BIMOS.Networking {
    public class NetworkRigManager : MonoBehaviour {
        public static NetworkRigManager Singleton {
            get; private set;
        }
        public Dictionary<ushort, BIMOSRig> RigCache {
            get; internal set;
        } = new();
        public ServerManager server;
        public ClientManager client;
        public bool AutoPhysicsRigGrips = true;
        public List<string> bannedObjects = new() {
            "UIRig",
            "EventSytem",
            "ThirdPersonCamera",
        };  
        public void Awake() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            Singleton = this;
            server = GetComponent<ServerManager>();
            client = GetComponent<ClientManager>();
            int layer = LayerMask.GetMask("BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer);
            BIMOSRig.OnRigSpawned += RigSpawned;
            RegisterBroadcasts();
        }

        private void RegisterBroadcasts() {
            server.RegisterBroadcast<SpawnRig>(SpawnRigHandler);
            client.RegisterBroadcast<SpawnRig>(SpawnRigClients);
            server.RegisterBroadcast<SyncRig>(SyncRigHandler);
            client.RegisterBroadcast<SyncRig>(SyncRigClients);
        }

        private void SyncRigClients(SyncRig message, Channel channel) {
            if (RigCache.TryGetValue(message.Id, out var rig))
                message.Deserialize(rig);
            else
                SpawnRigClients(new(message.Id), channel);
        }

        private void SyncRigHandler(NetworkConnection connection, SyncRig rig, Channel channel) {
            rig.Id = (ushort)connection.ClientId;
            server.BroadcastExcept(connection, rig);
        }

        private void SpawnRigClients(SpawnRig broadcast, Channel channel) {
            var rig = Instantiate(BIMOSRig.Instance);
            rig.enabled = false;
            rig.ControllerRig.SetActive(false);
            rig.GetComponents<EventSystem>().ToList().ForEach(e => Destroy(e.gameObject));
            rig.GetComponents<Camera>().ToList().ForEach(e => Destroy(e));
            if (AutoPhysicsRigGrips) {
                rig.AddComponent<PhysicsRigGrips>().physicsRig = rig.PhysicsRig;
            }
            RigCache.Add(broadcast.Id, rig);
        }

        private void SpawnRigHandler(NetworkConnection connection, SpawnRig rig, Channel channel) {
            rig.Id = (ushort)connection.ClientId;
            server.BroadcastExcept(connection, rig);
        }

        private void RigSpawned(object sender, BIMOSRig e) {
            client.Broadcast<SpawnRig>(new());
        }

        private void FixedUpdate() {
            if (!client.Started)
                return;
            SyncBIMOSRig();
        }

        private void SyncBIMOSRig() {
            var broadcast = new SyncRig();
            broadcast.Serialize(BIMOSRig.Instance);
            client.Broadcast(broadcast);
        }
    }

    public struct SpawnRig : IBroadcast {
        public ushort Id {
            get; set;
        }
        public SpawnRig(ushort id) {
            Id = id;
        }
    }

    public struct SyncRig : IBroadcast {
        public ushort Id {
            get; set;
        }
        public Vector3[] poses;
        public Quaternion[] rotations;

        public void Serialize(BIMOSRig rig) {
            if (rig == null)
                return;
            poses = new Vector3[4];
            rotations = new Quaternion[4];
            poses[0] = rig.ControllerRig.Transforms.Camera.position;
            poses[1] = rig.ControllerRig.Transforms.LeftController.position;
            poses[2] = rig.ControllerRig.Transforms.RightController.position;
            poses[3] = rig.PhysicsRig.Rigidbodies.Pelvis.position;

            rotations[0] = rig.ControllerRig.Transforms.Camera.rotation;
            rotations[1] = rig.ControllerRig.Transforms.LeftController.rotation;
            rotations[2] = rig.ControllerRig.Transforms.RightController.rotation;
            rotations[3] = rig.PhysicsRig.Rigidbodies.Pelvis.rotation;
        }

        public void Deserialize(BIMOSRig rig) {
            if (rig == null)
                return;
            poses = new Vector3[4];
            rotations = new Quaternion[4];
            float t = 40 * Time.deltaTime;
            rig.ControllerRig.Transforms.Camera.position.Lerp(poses[0], t);
            rig.ControllerRig.Transforms.LeftController.position.Lerp(poses[1], t);
            rig.ControllerRig.Transforms.RightController.position.Lerp(poses[2], t);
            rig.PhysicsRig.Rigidbodies.Pelvis.position.Lerp(poses[3], t);

            rig.ControllerRig.Transforms.Camera.rotation.Lerp(rotations[0], t);
            rig.ControllerRig.Transforms.LeftController.rotation.Lerp(rotations[1], t);
            rig.ControllerRig.Transforms.RightController.rotation.Lerp(rotations[2], t);
            rig.PhysicsRig.Rigidbodies.Pelvis.rotation.Lerp(rotations[3], t);
        }
    }
}
