using UnityEngine;
using FishNet.Object;
using KadenZombie8.BIMOS.Rig;
namespace KadenZombie8.BIMOS.Networking {
    [DefaultExecutionOrder(-2), DisallowMultipleComponent]
    public class NetworkRig : NetworkBehaviour {
        public BIMOSRig rig;
        public override void OnStartClient() {
            base.OnStartClient();
            rig = GetComponent<BIMOSRig>();
            rig.enabled = IsOwner;
            if (!IsOwner) {
                rig.ControllerRig.gameObject.SetActive(false);
                rig.PhysicsRig.enabled = false;
            }
        }
    }
}
