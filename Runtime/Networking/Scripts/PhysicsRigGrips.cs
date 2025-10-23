using FishNet.Object;
using KadenZombie8.BIMOS.Rig;
using KadenZombie8.BIMOS.Rig.Movement;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [DisallowMultipleComponent]
    public class PhysicsRigGrips : NetworkBehaviour
    {
        public PhysicsRig physicsRig;
        public override void OnStartClient() {
            base.OnStartClient();
            if (IsOwner)
                return;
            physicsRig.Colliders.Body.AddComponent<AutoGrabbable>();
            physicsRig.Colliders.Head.AddComponent<AutoGrabbable>();
            physicsRig.Colliders.LeftHand.AddComponent<AutoGrabbable>();
            physicsRig.Colliders.RightHand.AddComponent<AutoGrabbable>();
            physicsRig.Colliders.LocomotionSphere.AddComponent<AutoGrabbable>();
        }
    }
}
