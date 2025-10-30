using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class NetworkView : MonoBehaviour
    {
        private static Dictionary<int, NetworkView> Views = new();
        public int id;
        void Start()
        {
            id = Views.AllocateId(this);
        }
    }
}
