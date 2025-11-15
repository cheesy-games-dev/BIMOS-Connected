using Mirror;
using KadenZombie8.BIMOS;
using KadenZombie8.BIMOS.Rig;
using UnityEngine;

public class GamemodePlayer : NetworkBehaviour
{
    public float MaxHealth = 30;
    [SyncVar] public float Health = new();
    [SyncVar] public uint Kills = new();
    [SyncVar] public uint Deaths = new();
    public BIMOSRig rig;
    public static GamemodePlayer LocalRig = new();

    public override void OnStartClient() {
        base.OnStartClient();
        rig ??= GetComponent<BIMOSRig>();
        if (isLocalPlayer) LocalRig = this;
    }

    [Server]
    public void NewDeath() {
        Deaths++;
        Respawn(connectionToClient);
    }

    [TargetRpc]
    public void Respawn(NetworkConnection conn) {
        GamemodeMarker.SpawnPlayer(this);
    }

    [Command]
    public void RequestKill() {
        Kills++;
    }

    [Command(requiresAuthority = false)]
    public void RequestDamage(float damage) {
        Health -= damage;
        if (Health <= 0) {
            NewDeath();
        }
        Health = Mathf.Clamp(Health, 0 , MaxHealth);
    }
}
