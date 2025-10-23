using Mirror;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class NetworkRigidbody : NetworkTransformUnreliable {
        public new Rigidbody rigidbody;
        [SyncVar] public Vector3 networkVelocity;
        [SyncVar] public Vector3 networkAngularVelocity;
        [SyncVar] public bool networkIsKinematic;
        protected override void Awake() {
            rigidbody = GetComponent<Rigidbody>();
        }
        public void FixedUpdate() {
            if (!authority) {
                rigidbody.linearVelocity = Vector3.MoveTowards(rigidbody.linearVelocity, networkVelocity, Time.fixedDeltaTime);
                rigidbody.angularVelocity = Vector3.MoveTowards(rigidbody.angularVelocity, networkAngularVelocity, Time.fixedDeltaTime);
                rigidbody.isKinematic = networkIsKinematic;
            }
            else {
                networkVelocity = rigidbody.linearVelocity;
                networkAngularVelocity = rigidbody.angularVelocity;
                networkIsKinematic = rigidbody.isKinematic;
            }
        }
    }
}
