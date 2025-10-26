using Mirror;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public interface IOwnershipEvents {
        [ClientRpc]
        public void OnOwnershipClient(NetworkConnectionToClient previous) {

        }
    }
}
