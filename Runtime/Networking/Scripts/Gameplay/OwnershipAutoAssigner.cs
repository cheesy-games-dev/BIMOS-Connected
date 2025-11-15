using UnityEngine;
using Mirror;

[DefaultExecutionOrder(-1)]
public class OwnershipAutoAssigner : NetworkBehaviour
{
    [Server]
    public override void OnStartClient() {
        base.OnStartClient();
        netIdentity.RemoveClientAuthority();
        netIdentity.AssignClientAuthority(NetworkServer.localConnection);
    }
}
