using Riptide;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    [DefaultExecutionOrder(-1)]
    public class NetworkView : MonoBehaviour
    {
        private static Dictionary<int, NetworkView> Views = new();
        public int Id {
            get; internal set;
        }
        public int Owner {
            get; internal set;
        }
        public bool IsMine {
            get {
                var connection = Network.Client.Connection;
                if (connection == null) return false;
                return connection.Id == Owner;
            }
        }
        public List<IObservable> Observables = new();
        void Start()
        {
            Id = Views.AllocateId(this);
            Observables = GetComponentsInChildren<IObservable>().ToList();
            Network.Client.MessageReceived += Client_MessageReceived;
            Network.Server.MessageReceived += Server_MessageReceived;
        }
        private void FixedUpdate() {
            foreach (var observable in Observables) {
                if (!observable.CanWrite)
                    continue;
                var message = observable.Serialize(Observables.IndexOf(observable));
                observable.Send(message);
            }
        }

        private void Server_MessageReceived(object sender, MessageReceivedEventArgs e) {

        }

        private void Client_MessageReceived(object sender, MessageReceivedEventArgs e) {
            if (e.MessageId == (ushort)MessageId.OwnershipChange) {
                Owner = e.Message.GetInt();
            }
            if (e.MessageId == (ushort)MessageId.Observable) {
                var observable = Observables[e.Message.GetInt()];
                observable.Deserialize(e.Message);
            }
        }

        [ServerCallback]
        public void GiveOwnership(ushort newOwner) {
            Owner = newOwner;
            SendOwnershipMessage();
        }
        [ServerCallback]
        public void RemoveOwnership() {
            Owner = -1;
            SendOwnershipMessage();
        }

        [ServerCallback]
        private void SendOwnershipMessage() {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)MessageId.OwnershipChange);
            message.AddInt(Owner);
            Network.Server.SendToAll(message);
        }
    }
    public interface IObservable {
        public abstract bool CanWrite {
            get;
        }
        public Message Serialize(int viewId);
        public void Deserialize(Message received);
        public void Send(Message message);
    }
}
