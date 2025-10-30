using Riptide;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [RequireComponent(typeof(Rigidbody))]
    public class NetworkRigidbody : MonoBehaviourNetwork, IObservable
    {
        public bool ClientAuthority = true;
        public MessageSendMode SendMode;
        private new Rigidbody rigidbody;
        public RigidbodyData data = new();
        public bool CanWrite => ClientAuthority ? View.IsMine && Network.Client.IsConnected : Network.Server.IsRunning;
        void Start() {
            rigidbody = GetComponent<Rigidbody>();
        }
        public void Deserialize(Message received) {
            if (CanWrite)
                return;
            data = received.GetSerializable<RigidbodyData>();
        }

        public void Send(Message message) {
            if(ClientAuthority) {
                Network.Client.Send(message);
            } else if(Network.Server.IsRunning) {
                Network.Server.SendToAll(message);
            }
        }
        public void FixedUpdate() {
            if (!CanWrite) {
                rigidbody.position = Vector3.MoveTowards(rigidbody.position, data.Position, Time.fixedDeltaTime);
                rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, data.Rotation, Time.fixedDeltaTime * 100.0f);
                rigidbody.linearVelocity = data.LinearVelocity;
                rigidbody.angularVelocity = data.AngularVelocity;
            }
        }
        public Message Serialize(int viewId) {
            if (!CanWrite)
                return null;
            Message message = Message.Create(SendMode, (ushort)MessageId.Observable);
            message.Add(viewId);
            message.Add(new RigidbodyData(rigidbody));
            return message;
        }
    }

    public struct RigidbodyData : IMessageSerializable {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;
        public RigidbodyData(Rigidbody rigidbody) {
            Position = rigidbody.position;
            Rotation = rigidbody.rotation;
            LinearVelocity = rigidbody.linearVelocity;
            AngularVelocity = rigidbody.angularVelocity;
        }

        public void Deserialize(Message message) {
            Position = message.GetVector3();
            Rotation = message.GetQuaternion();
            LinearVelocity = message.GetVector3();
            AngularVelocity = message.GetVector3();
        }

        public void Serialize(Message message) {
            message.Add(Position);
            message.Add(Rotation);
            message.Add(LinearVelocity);
            message.Add(AngularVelocity);
        }
    }
}
