using Riptide;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace HL.Networking {
    public class NetworkView : MonoBehaviour
    {
        public ushort Id = 0;

        public ushort Owner = 0;

        public bool OwnershipTransfer = true;
        public bool ActiveOnDisconnect = true;
        public bool RuntimeSearch = true;
        public List<IObservable> ObservedComponents = new();

        public bool IsMine => Owner == Network.Client.Id;

        private void Start() {
            SearchObservables();
            Network.AllocateViewID(this);
        }

        private void SearchObservables() {
            if (RuntimeSearch) {
                ObservedComponents.AddRange(GetComponentsInChildren<IObservable>());
            }
        }
    }

    public interface IObservable {
        public void Serialize(Message message);
        public void Deserialize(Message message);
    }
}
