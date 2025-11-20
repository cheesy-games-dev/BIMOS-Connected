using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System.Collections.Generic;
namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : OwnershipAutoAssigner {
        public static Dictionary<int, NetworkGrip> Instances { get; private set; } = new Dictionary<int, NetworkGrip>();
        public static int NextId { get; private set; } = 0;
        public Grabbable grabbable;
        private void Awake() {
            grabbable = GetComponent<Grabbable>();
            grabbable.OnGrab.AddListener(OnGrabbed);
            grabbable.OnRelease.AddListener(OnReleased);
        }
        private void OnGrabbed() {

        }
        private void OnReleased() {
        }
    }
}