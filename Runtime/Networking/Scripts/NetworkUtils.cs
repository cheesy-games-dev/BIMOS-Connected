using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static class NetworkUtils
    {
        public static int AllocateId<T>(this Dictionary<int, T> dictionary, T value) {
            int id = 0;
            while (dictionary.ContainsKey(id)) {
                id++;
            }
            dictionary.Add(id, value);
            return id;
        }
    }
}
