using KadenZombie8.BIMOS.Rig;
using Mirror;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [DefaultExecutionOrder(-2)]
    public class NetworkRig : NetworkBehaviour
    {
        [SyncVar] public NetworkRigInfo RigInfo;

        BIMOSRig rig;
        private void Awake() {
            rig = GetComponent<BIMOSRig>();
            rig.enabled = false;
            syncDirection = SyncDirection.ServerToClient;
        }

        public override void OnStartClient() {
            if (isLocalPlayer) {
                rig.enabled = true;
                NetworkManager.singleton.LocalRig = this;
            }
            else {
                rig.ControllerRig.SetActive(false);
            }
        }
    }
}
