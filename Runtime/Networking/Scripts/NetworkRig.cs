using FishNet.Object;
using KadenZombie8.BIMOS.Rig;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [DefaultExecutionOrder(-2)]
    public class NetworkRig : NetworkBehaviour
    {
        Networking.NetworkManager networkManager;
        BIMOSRig rig;
        private void Awake() {
            rig = GetComponent<BIMOSRig>();
            rig.enabled = false;
            networkManager = Networking.NetworkManager.Instance;
            networkManager.RigCache.Add(Owner, this);
        }

        public override void OnStartClient() {
            if (IsOwner) {
                rig.enabled = true;
                networkManager.LocalRig = this;
            }
            else {
                rig.ControllerRig.SetActive(false);
            }
        }
    }
}
