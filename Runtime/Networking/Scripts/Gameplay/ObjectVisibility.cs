using UnityEngine;
using System.Collections.Generic;
using System;
using FishNet.Object;

namespace KadenZombie8.BIMOS.Networking {
    [DefaultExecutionOrder(1000)]
    public abstract class ObjectVisibility<T> : NetworkBehaviour where T : UnityEngine.Object {
        public List<VisibleObject> references;
        private void Start() {
            Refresh();
        }
        public void Refresh() {
            bool isHost = IsServerStarted;
            bool isClient = IsClientStarted;
            bool isOwner = IsOwner;
            Predicate<VisibleObject> disablePredicate = x => (x.showOn.HasFlag(Targets.Server) && !isHost) || (x.showOn.HasFlag(Targets.Clients) && !isClient) || (x.showOn.HasFlag(Targets.Owner) && !isOwner);
            references.FindAll(disablePredicate).ForEach(Disable);
        }

        protected abstract void Enable(VisibleObject @object);
        protected abstract void Disable(VisibleObject @object);

        [Serializable]
        public struct VisibleObject {
            public T obj;
            public bool showOnAuth;
            public Targets showOn;
        }

        [Flags]
        public enum Targets : ushort {
            Server,
            Clients,
            Owner,
        }
    }


}
