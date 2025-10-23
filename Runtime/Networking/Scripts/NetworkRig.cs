using UnityEngine;
using FishNet.Object;
using KadenZombie8.BIMOS.Rig;
namespace KadenZombie8.BIMOS.Networking {
    public class NetworkRig : NetworkBehaviour {
        public BIMOSRig MyRig {
            get; internal set;
        }

        public static NetworkRig LocalNetworkRig;
        public override void OnStartClient() {
            base.OnStartClient();
            MyRig.PhysicsRig.enabled = false;
            if (IsOwner) {
                MyRig.AnimationRig.gameObject.SetActive(false);
            }
        }
        private void Update() {
            if (!IsOwner)
                return;
            MyRig.PhysicsRig.Rigidbodies.Pelvis.position = BIMOSRig.Instance.PhysicsRig.Rigidbodies.Pelvis.position;
            MyRig.PhysicsRig.Rigidbodies.Pelvis.rotation = BIMOSRig.Instance.PhysicsRig.Rigidbodies.Pelvis.rotation;
        }
    }
}