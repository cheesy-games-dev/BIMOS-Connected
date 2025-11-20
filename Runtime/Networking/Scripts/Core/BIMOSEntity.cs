using KadenZombie8.Pooling;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class BIMOSEntity : MonoBehaviour
    {
        public static event Action<BIMOSEntity> OnEntitySpawned;
        public static event Action<BIMOSEntity> OnEntityDespawned;

        public static Dictionary<int, BIMOSEntity> Entities;

        private void Start() {

        }
    }
}
