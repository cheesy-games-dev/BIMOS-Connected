using Riptide;
using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class NetworkEntityHost : MonoBehaviour
    {
        public NetworkEntity Entity;
        public bool AutoSpawn = true;
        public DespawnMode DespawnMode;

        private void Start() {
            if(AutoSpawn) Spawn();
        }

        private static void Spawn() {
            if (!LayerProcessor.singleton.Server.IsRunning)
                return;
        }
    }
}
