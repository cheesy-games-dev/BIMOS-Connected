using KadenZombie8.BIMOS.Rig;
using Mirror;
using System.Linq;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [DefaultExecutionOrder(-2)]
    public class NetworkRig : NetworkBehaviour
    {
        BIMOSRig rig;
        public GameObject[] OwnerOnlyObjects;
        private void Awake() {
            rig = GetComponent<BIMOSRig>();
            rig.enabled = false;
            int layer = LayerMask.GetMask("BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer);
        }

        public override void OnStartClient() {
            if (isLocalPlayer) {
                rig.enabled = true;
                BIMOSNetworkManager.Singleton.LocalRig = this;
            }
            else {
                rig.ControllerRig.SetActive(false);
                OwnerOnlyObjects.ToList().ForEach(x=>x.gameObject.SetActive(false));
            }
        }
    }
}
