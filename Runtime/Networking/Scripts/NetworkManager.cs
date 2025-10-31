using UnityEngine;
using System.Collections.Generic;
using KadenZombie8.BIMOS.Rig;
using KadenZombie8.BIMOS.Rig.Movement;
using KadenZombie8.BIMOS.Rig.Spawning;
using Fusion;
namespace KadenZombie8.BIMOS.Networking {
    public class NetworkManager : MonoBehaviour {
        public static NetworkManager Instance {
            get; private set;
        }
        public BIMOSNetworkRunner runnerPrefab;
        private void Awake() {
            InitializeOnce();
        }
        private void InitializeOnce() {
            Instance = this;
            int layer = LayerMask.GetMask("Player", "BIMOSRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            if (!BIMOSNetworkRunner.Instance) {
                Instantiate(runnerPrefab);
            }
        }
    }
}
