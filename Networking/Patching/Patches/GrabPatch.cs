using UnityEngine;
using HarmonyLib;
using KadenZombie8.BIMOS.Rig;

namespace KadenZombie8.BIMOS
{
    [HarmonyPatch(typeof(Grabbable))]
    public static class GrabPatch
    {
        [HarmonyPatch(nameof(Grabbable.Grab))]
        [HarmonyPrefix]
        public static void OnGrabPrefix(Grabbable __instance, Hand hand)
        {
            var rb = __instance.Collider.attachedRigidbody;
            if (!rb) return;
            if (rb.isKinematic) return;
            GrabbableManager.Instance.SendGrabMessage(__instance, hand);
        }
    }
}
