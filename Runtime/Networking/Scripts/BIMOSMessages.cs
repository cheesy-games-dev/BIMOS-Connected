using KadenZombie8.BIMOS.Rig.Movement;
using Riptide;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    public enum ServerMessages : ushort {
        SpawnPlayer = 1,
        SyncPlayer,
        GripTakeover,
    }

    public enum ClientMessages : ushort {
        PlayerSpawned,
        PlayerSynced,
    }
    public struct GripTakeover : IMessageSerializable {
        public int netId;
        public GripTakeover(int netId) {
            this.netId = netId;
        }

        public void Deserialize(Message message) {
            netId = message.GetInt();
        }

        public void Serialize(Message message) {
            message.Add(netId);
        }
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