using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System;

namespace KadenZombie8.BIMOS.Networking {
    [DefaultExecutionOrder(1000)]
    public abstract class ObjectVisibility<T> : NetworkBehaviour where T : UnityEngine.Object {
        public List<VisibleObject> references;
        private void Start() {
            Refresh();
        }
        public void Refresh() {
            Predicate<VisibleObject> enablePredicate = x => (x.showOn.HasFlag(Targets.Server) && isServer) || (x.showOn.HasFlag(Targets.Clients) && isClient) || (x.showOn.HasFlag(Targets.Owner) && isOwned);
            references.FindAll(enablePredicate).ForEach(Enable);
            Predicate<VisibleObject> disablePredicate = x => (x.showOn.HasFlag(Targets.Server) && !isServer) || (x.showOn.HasFlag(Targets.Clients) && !isClient) || (x.showOn.HasFlag(Targets.Owner) && !isOwned);
            references.FindAll(disablePredicate).ForEach(Disable);
        }

        protected abstract void Enable(VisibleObject @object);
        protected abstract void Disable(VisibleObject @object);

        [Serializable]
        public struct VisibleObject {
            public T obj;
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
