using UnityEngine;
using System.Collections.Generic;
using KadenZombie8.BIMOS.Rig;
using KadenZombie8.BIMOS.Rig.Movement;
using Riptide;

namespace KadenZombie8.BIMOS.Networking {
    public class BIMOSManager : MonoBehaviour {
        public static BIMOSManager singleton;
        public Dictionary<int, BIMOSRig> BIMOSRigs;
        public BIMOSRig BIMOSRigPrefab;
        private void Awake() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            singleton = this;
            int layer = LayerMask.GetMask("Player", "BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            InstanceFinder.ClientManager.RegisterBroadcast<SpawnPlayer>(SpawnPlayerCallback);
            InstanceFinder.ClientManager.RegisterBroadcast<SyncPlayer>(SyncPlayerClientCallback);
            InstanceFinder.ServerManager.RegisterBroadcast<SyncPlayer>(SyncPlayerServerCallback, false);
            InstanceFinder.SceneManager.OnClientLoadedStartScenes += SceneManager_OnClientLoadedStartScenes;
        }

        private void SyncPlayerClientCallback(SyncPlayer player, Channel channel) {
            var rig = BIMOSRigs[player.clientId];
            player.SetPhysicsRig(rig.PhysicsRig);
        }
        private void SyncPlayerServerCallback(NetworkConnection connection, SyncPlayer player, Channel channel) {
            player.clientId = connection.ClientId;
            InstanceFinder.ServerManager.BroadcastExcept(connection, player);
        }

        private void SpawnPlayerCallback(SpawnPlayer player, Channel channel) {
            var rig = Instantiate(BIMOSRigPrefab);
            rig.enabled = false;
            rig.ControllerRig.gameObject.SetActive(false);
            rig.PhysicsRig.enabled = false;
            BIMOSRigs.Add(player.clientId, rig);
        }

        private void Update() {
            SyncPlayer player = new(BIMOSRig.Instance.PhysicsRig, -1);
            Message message = Message.Create(MessageSendMode.Unreliable, (ushort)ServerMessages.SyncPlayer);
            message.Add(player);
            Network.Client.Send(message);
        }

        private void SceneManager_OnClientLoadedStartScenes(NetworkConnection conn, bool asServer) {
            SpawnPlayer player = new SpawnPlayer {
                clientId = conn.ClientId
            };
            InstanceFinder.ServerManager.BroadcastExcept(conn, player);
        }
    }

    public enum ServerMessages : ushort {
        SpawnPlayer = 1,
        SyncPlayer = 2,
    }

    public enum ClientMessages : ushort {
        PlayerSpawned = 1,
        PlayerSynced = 2,
    }

    public struct SpawnPlayer : IMessageSerializable {
        public int clientId;

        public void Deserialize(Message message) {
            clientId = message.GetInt();
        }

        public void Serialize(Message message) {
            message.Add(clientId);
        }
    }

    public struct SyncPlayer : IMessageSerializable {
        public int clientId;
        public TransformStruct locoball;
        public TransformStruct knee;
        public TransformStruct pelvis;
        public TransformStruct head;
        public TransformStruct leftHand;
        public TransformStruct rightHand;
        public SyncPlayer(PhysicsRig rig, int _clientId) {
            clientId = _clientId;
            locoball = new(rig.Rigidbodies.LocomotionSphere);
            knee = new(rig.Rigidbodies.Knee);
            pelvis = new(rig.Rigidbodies.Pelvis);
            head = new(rig.Rigidbodies.Head);
            leftHand = new(rig.Rigidbodies.LeftHand);
            rightHand = new(rig.Rigidbodies.RightHand);
        }

        public void Deserialize(Message message) {
            throw new System.NotImplementedException();
        }

        public void Serialize(Message message) {
            message.Add(clientId);
            message.Add(locoball);
            message.Add(knee);
            message.Add(pelvis);
            message.Add(head);
            message.Add(leftHand);
            message.Add(rightHand);
        }

        public void SetPhysicsRig(PhysicsRig rig) {
            rig.Rigidbodies.LocomotionSphere.Copy(locoball);
            rig.Rigidbodies.Knee.Copy(knee);
            rig.Rigidbodies.Pelvis.Copy(pelvis);
            rig.Rigidbodies.Head.Copy(head);
            rig.Rigidbodies.LeftHand.Copy(leftHand);
            rig.Rigidbodies.RightHand.Copy(rightHand);
        }

        public struct TransformStruct : IMessageSerializable {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
            public Vector3 velocity;
            public Vector3 angularVelocity;
            public TransformStruct(Rigidbody body) {
                position = body.transform.position;
                rotation = body.transform.rotation;
                scale = body.transform.localScale;
                velocity = body.linearVelocity;
                angularVelocity = body.angularVelocity;
            }
            public TransformStruct(Transform transform) {
                position = transform.position;
                rotation = transform.rotation;
                scale = transform.localScale;
                velocity = Vector3.zero;
                angularVelocity = Vector3.zero;
            }

            public void Deserialize(Message message) {
                position = message.GetVector3();
                rotation = message.GetQuaternion();
                scale = message.GetVector3();
                velocity = message.GetVector3();
                angularVelocity = message.GetVector3();
            }

            public void Serialize(Message message) {
                message.Add(position);
                message.Add(rotation);
                message.Add(scale);
                message.Add(velocity);
                message.Add(angularVelocity);
            }
        }
    }
}
