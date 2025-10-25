using UnityEngine;
using KadenZombie8.BIMOS.Rig.Spawning;
using Mirror;
using System;
using System.Collections.Generic;

namespace KadenZombie8.BIMOS.Networking
{
    public class BIMOSManager : NetworkManager
    {
        public static new BIMOSManager singleton;
        public override void Awake() {
            InitializeOnce();
        }

        private void InitializeOnce() {
            singleton = this;
            int layer = LayerMask.NameToLayer("Player");
            Physics.IgnoreLayerCollision(layer, layer, true);
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn) {
            Transform startPos = SpawnPointManager.Instance.SpawnPoint.transform;
            GameObject player = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab);

            // instantiating a "Player" prefab gives it the name "Player(clone)"
            // => appending the connectionId is WAY more useful for debugging!
            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            NetworkServer.AddPlayerForConnection(conn, player);
        }
    }
}
