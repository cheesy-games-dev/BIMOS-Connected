using UnityEngine;
using static KadenZombie8.BIMOS.Networking.SyncPlayer;

namespace KadenZombie8.BIMOS.Networking {
    public static class BIMOSExtensions {
        public static void Copy(this Rigidbody body, TransformStruct ts) {
            body.transform.position = ts.position;
            body.transform.rotation = ts.rotation;
            body.transform.localScale = ts.scale;
            body.linearVelocity = ts.velocity;
            body.angularVelocity = ts.angularVelocity;
        }

        public static void Copy(this Transform transform, TransformStruct ts) {
            transform.position = ts.position;
            transform.rotation = ts.rotation;
            transform.localScale = ts.scale;
        }
    }
}
