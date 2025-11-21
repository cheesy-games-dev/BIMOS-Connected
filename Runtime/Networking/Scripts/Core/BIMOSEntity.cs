using KadenZombie8.Pooling;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class BIMOSEntity : MonoBehaviour
    {
        public List<BIMOSBody> bodies = new();
        public static List<BIMOSEntity> Pool = new();
        public bool Poolee = true;
        private void Start() {
            bodies = GetComponentsInChildren<BIMOSBody>().ToList();
        }
        private void OnEnable() {
            if (Poolee) Pool?.Add(this);
        }

        private void OnDisable() {
            if(Poolee) Pool?.Remove(this);
        }
    }
}
