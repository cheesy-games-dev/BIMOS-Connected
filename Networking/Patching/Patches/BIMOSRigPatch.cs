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
        [HarmonyPatch("Awake")]
        [HarmonyPrefix]
        public static void OnAwakePrefix(BIMOSRig __instance)
        {
            int layer = LayerMask.GetMask("Player", "Rig", "BIMOSRig", "BIPEDRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            if (BIMOSRig.Instance != null) {

            }
            else {
                Object.Destroy(__instance.GetComponentInChildren<EventSystem>().gameObject);
                Object.Destroy(__instance.GetComponentInChildren<SettingsMenu>().gameObject);
                foreach (var camera in __instance.GetComponentsInChildren<Camera>())
                    Object.Destroy(camera);
            }
        }
    }   
}
