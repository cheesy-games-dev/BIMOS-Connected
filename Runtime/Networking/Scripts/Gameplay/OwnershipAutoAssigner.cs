using UnityEngine;
using FishNet.Object;

[DefaultExecutionOrder(-1)]
public class OwnershipAutoAssigner : NetworkBehaviour
{
    [Server]
    public override void OnStartClient() {
        base.OnStartClient();
        NetworkObject.RemoveOwnership();
        NetworkObject.GiveOwnership(LocalConnection);
    }
}
