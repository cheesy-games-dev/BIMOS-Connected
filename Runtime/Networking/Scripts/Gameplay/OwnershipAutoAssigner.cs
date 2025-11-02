using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

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
