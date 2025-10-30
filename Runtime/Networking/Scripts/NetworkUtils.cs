using System.Collections.Generic;

namespace KadenZombie8.BIMOS.Networking
{
    public static class NetworkUtils
    {
        public static int AllocateId<T>(this Dictionary<int, T> dictionary, T value) {
            int id = 0;
            while (dictionary.ContainsKey(id)) {            
                id++;
                if (dictionary[id].Equals(value)) {
                    break;
                }
            }
            dictionary.TryAdd(id, value);
            return id;
        }
    }
}
