using UnityEngine;
using HarmonyLib;
using KadenZombie8.BIMOS.Rig;
using Mirror;
using KadenZombie8.BIMOS.Networking;
using System;

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
            NetworkIdentity networkObject = __instance.GetNetworkIdentity();
            if (!networkObject) return;
            if (!networkObject.IsOwner()) return;
            MonoBehaviour.Destroy(__instance.ControllerRig);
            MonoBehaviour.Destroy(__instance.GetComponentInChildren<SettingsMenu>().gameObject);
            foreach (var camera in __instance.GetComponentsInChildren<Camera>()) MonoBehaviour.Destroy(camera);
        }

        public static void ConvertToNetworkRig(BIMOSRig rig)
        {
            if (!rig.GetNetworkIdentity()) rig.gameObject.AddComponent<NetworkIdentity>();
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
                AddNetworkTransform<NetworkRigidbodyUnreliable>(transforms);
            }
            if (rig.ControllerRig)
            {
                var aRig = rig.ControllerRig.Transforms;
                var transforms = new Transform[]{
                    aRig.LeftController,
                    aRig.RightController,
                    aRig.Camera,
                };
                AddNetworkTransform<NetworkTransformUnreliable>(transforms);
            }
        }

        public static void AddNetworkTransform<T>(Transform[] transforms) where T : NetworkTransformBase
        {
            Type type = typeof(T);
            foreach (var transform in transforms)
            {
                if (!transform.GetComponent(type)) transform.gameObject.AddComponent(type);
            }
        }
    }

    
}
