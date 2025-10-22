using Riptide;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static class NetworkExtensions
    {
        public static bool IsLocal(this Connection connection) => connection == Network.Client.Connection;
    }
}
