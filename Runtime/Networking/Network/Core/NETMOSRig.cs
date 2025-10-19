using FishNet.Object;
using KadenZombie8.BIMOS.Rig;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class NETMOSRig : NetworkBehaviour
    {
        public override void OnStartNetwork() {
            base.OnStartNetwork();
            int layer = LayerMask.GetMask("Player", "Rig", "BIMOSRig", "BIPEDRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            if (Owner.IsLocalClient) {
                BIMOSRig.Instance = __instance;
                BIMOSLogger.Log($"{__instance.name} is Local Rig");
            }
            else {
                BIMOSLogger.Log($"{__instance.name} is Online Rig");
                Object.Destroy(__instance.GetComponentInChildren<EventSystem>().gameObject);
                Object.Destroy(__instance.GetComponentInChildren<SettingsMenu>().gameObject);
                foreach (var camera in __instance.GetComponentsInChildren<Camera>())
                    Object.Destroy(camera);
            }
        }
    }
}
