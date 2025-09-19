using UnityEngine;
using HarmonyLib;
using KadenZombie8.BIMOS.Rig;
using FishNet.Object;
using FishNet.Component.Transforming;

namespace KadenZombie8.BIMOS
{
    [HarmonyPatch(typeof(BIMOSRig))]
    public static class BIMOSRigPatch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPrefix]
        public static void OnAwakePrefix(BIMOSRig __instance)
        {
            NetworkObject networkObject = __instance.GetComponentInParent<NetworkObject>();
            if (!networkObject) return;
            if (!networkObject.IsOwner) return;
            MonoBehaviour.Destroy(__instance.ControllerRig);
            MonoBehaviour.Destroy(__instance.GetComponentInChildren<SettingsMenu>().gameObject);
            foreach (var camera in __instance.GetComponentsInChildren<Camera>()) MonoBehaviour.Destroy(camera);
        }

        public static void ConvertToNetworkRig(BIMOSRig rig)
        {
            if (!rig.GetComponent<NetworkObject>()) rig.gameObject.AddComponent<NetworkObject>();
            if (rig.PhysicsRig)
            {
                var aRig = rig.PhysicsRig.Rigidbodies;
                var transforms = new Transform[]{
                    aRig.RightHand.transform,
                    aRig.LeftHand.transform,
                    aRig.LocomotionSphere.transform,
                    aRig.Knee.transform,
                    aRig.Head.transform,
                    aRig.Pelvis.transform,
                };
                AddNetworkTransform(transforms);
            }
            if (rig.ControllerRig)
            {
                var aRig = rig.ControllerRig.Transforms;
                var transforms = new Transform[]{
                    aRig.LeftController,
                    aRig.RightController,
                    aRig.Camera,
                };
                AddNetworkTransform(transforms);
            }
        }

        public static void AddNetworkTransform(Transform[] transforms)
        {
            foreach (var transform in transforms)
            {
                if (!transform.GetComponent<NetworkTransform>()) transform.gameObject.AddComponent<NetworkTransform>();
            }
        }
    }

    
}
