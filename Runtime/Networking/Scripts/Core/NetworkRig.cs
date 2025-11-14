using FishNet.Object;
using KadenZombie8.BIMOS.Rig;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [DefaultExecutionOrder(-2)]
    public class NetworkRig : NetworkBehaviour
    {
        BIMOSRig rig;
        private void Awake() {
            rig = GetComponent<BIMOSRig>();
            rig.enabled = false;
        }

        public override void OnStartClient() {
            if (IsOwner) {
                rig.enabled = true;
                Manager.singleton.LocalRig = this;
            }
            else {
                rig.ControllerRig.SetActive(false);
            }
        }
    }
}
