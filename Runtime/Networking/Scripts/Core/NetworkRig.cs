using FishNet.Object;
using KadenZombie8.BIMOS.Rig;
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
            if (IsOwner) {
                rig.enabled = true;
                Manager.singleton.LocalRig = this;
            }
            else {
                rig.ControllerRig.SetActive(false);
                OwnerOnlyObjects.ToList().ForEach(x=>x.gameObject.SetActive(false));
            }
        }
    }
}
