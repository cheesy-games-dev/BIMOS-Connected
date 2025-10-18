using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadenZombie8.BIMOS.Networking {
    public enum ClientToServer : ushort {
        GeneralMessage,
        LobbyMessage,
        GameMessage,
        BIMOSMessage,
    }

    public enum ServerToClient : ushort {
        SceneChange = 0,
        SpawnMessage,
    }
}
