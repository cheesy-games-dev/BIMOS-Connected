using Riptide;
using UnityEngine;

namespace HL.Networking {
    public static class NetworkExtensions
    {
        public static bool IsLocal(this Connection connection) => connection == Network.Client.Connection;
    }
}
