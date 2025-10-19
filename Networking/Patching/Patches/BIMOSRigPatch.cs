using UnityEngine;
using HarmonyLib;
using KadenZombie8.BIMOS.Rig;
using KadenZombie8.BIMOS.Networking;
using UnityEngine.EventSystems;

namespace KadenZombie8.BIMOS
{
    [HarmonyPatch(typeof(BIMOSRig))]
    public static class BIMOSRigPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void StartPrefix(BIMOSRig __instance)
        {
            int layer = LayerMask.GetMask("Player", "Rig", "BIMOSRig", "BIPEDRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            if (__instance.GetNetworkObject().IsOwner) {
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
