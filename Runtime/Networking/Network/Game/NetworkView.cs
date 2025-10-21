using Riptide;
using UnityEngine;
using System.Collections.Generic;

namespace KadenZombie8.BIMOS.Networking
{
    public class NetworkView : MonoBehaviour
    {
        public static Dictionary<ushort, NetworkView> Views = new Dictionary<ushort, NetworkView>();

        public ushort Id {
            get; private set;
        }

        public Connection Owner {
            get; private set;
        }

        public bool CanTransfer { get; private set; }

        public bool IsMine => Owner.IsLocal();
    }
}
