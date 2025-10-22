using Riptide;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace HL.Networking {
    public class NetworkView : MonoBehaviour
    {
        public static Dictionary<ushort, NetworkView> Views = new Dictionary<ushort, NetworkView>();

        public ushort Id;

        public Connection Owner;

        public bool OwnershipTransfer;

        public bool RuntimeSearch = true;
        public List<IObservable> ObservedComponents = new();

        public bool IsMine => Owner.IsLocal();

        private void Start() {
            SearchObservables();
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
