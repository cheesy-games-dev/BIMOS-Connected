using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {

    [DefaultExecutionOrder(-2)]
    public class MonoBehaviourNetwork : MonoBehaviour
    {
        public NetworkView View {
            get; internal set;
        }

        private void Awake() {
            View = GetComponentInParent<NetworkView>();
        }
    }
}
