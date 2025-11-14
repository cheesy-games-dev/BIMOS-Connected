using FishNet.CodeGenerating;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using KadenZombie8.BIMOS;
using KadenZombie8.BIMOS.Rig;
using UnityEngine;

public class GamemodePlayer : NetworkBehaviour
{
    public float MaxHealth = 30;
    [AllowMutableSyncType] public SyncVar<float> Health = new();
    [AllowMutableSyncType] public SyncVar<uint> Kills = new();
    [AllowMutableSyncType] public SyncVar<uint> Deaths = new();
    public BIMOSRig rig;
    public static GamemodePlayer LocalRig = new();

    public override void OnStartClient() {
        base.OnStartClient();
        rig ??= GetComponent<BIMOSRig>();
        if (IsOwner) LocalRig = this;
    }

    [Server]
    public void NewDeath() {
        Deaths.Value++;
        Respawn(Owner);
    }

    [TargetRpc]
    public void Respawn(NetworkConnection conn) {
        GamemodeMarker.SpawnPlayer(this);
    }

    [ServerRpc]
    public void RequestKill() {
        Kills.Value++;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestDamage(float damage) {
        Health.Value -= damage;
        if (Health.Value <= 0) {
            NewDeath();
        }
        Health.Value = Mathf.Clamp(Health.Value, 0 , MaxHealth);
    }
}
