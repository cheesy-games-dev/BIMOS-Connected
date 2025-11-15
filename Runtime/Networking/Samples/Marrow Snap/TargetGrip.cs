using KadenZombie8.BIMOS.Rig;
using UnityEngine;

namespace KadenZombie8.BIMOS
{
    [RequireComponent(typeof(SphereCollider))]
    public class TargetGrip : Grabbable {
        public SphereCollider SphereCollider => Collider as SphereCollider;
        public override float CalculateRank(Hand hand) => base.CalculateRank(hand) * 3f;

        public override void AlignHand(Hand hand, out Vector3 position, out Quaternion rotation) {
            var offset = hand.Handedness == Handedness.Right ? SphereCollider.radius : -SphereCollider.radius;
            position = transform.TransformPoint(hand.PalmTransform.InverseTransformPoint(hand.PhysicsHandTransform.position) - (Vector3.forward*offset));
            var transformRotation = hand.Handedness == Handedness.Right ? transform.rotation : Quaternion.Inverse(transform.rotation);
            rotation = transformRotation * Quaternion.Inverse(hand.PalmTransform.rotation) * hand.PhysicsHandTransform.rotation;
        }
    }
}
