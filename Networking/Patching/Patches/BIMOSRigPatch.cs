using UnityEngine;
using HarmonyLib;
using KadenZombie8.BIMOS.Rig;
using Mirror;
using KadenZombie8.BIMOS.Networking;

namespace KadenZombie8.BIMOS
{
    [HarmonyPatch(typeof(BIMOSRig))]
    public static class BIMOSRigPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void OnStartPrefix(BIMOSRig __instance)
        {
            int layer = LayerMask.GetMask("Player", "Rig", "BIMOSRig", "BIPEDRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            NetworkIdentity networkObject = __instance.GetNetworkIdentity();
            if (networkObject.isLocalPlayer) {
                BIMOSRig.Instance = __instance;
            }
            else {
                Object.Destroy(__instance.ControllerRig);
                Object.Destroy(__instance.GetComponentInChildren<SettingsMenu>().gameObject);
                foreach (var camera in __instance.GetComponentsInChildren<Camera>())
                    Object.Destroy(camera);
            }
        }
    }   
}
