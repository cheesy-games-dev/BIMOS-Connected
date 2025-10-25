using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using Mirror;
namespace KadenZombie8.BIMOS.Networking {
    [DefaultExecutionOrder(-2), DisallowMultipleComponent]
    public class NetworkRig : NetworkBehaviour {
        public BIMOSRig rig;
        public override void OnStartClient() {
            base.OnStartClient();
            rig = GetComponent<BIMOSRig>();
            rig.enabled = isLocalPlayer;
            if (!rig.enabled) {
                rig.ControllerRig.gameObject.SetActive(false);
                rig.PhysicsRig.enabled = false;
            }
        }
    }
}
