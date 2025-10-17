using System.Collections.Generic;
using System.Linq;
using KadenZombie8.BIMOS.Networking;
using KadenZombie8.BIMOS.Rig;
using Mirror;
using UnityEditor.VersionControl;
using UnityEngine;

namespace KadenZombie8.BIMOS
{
    public class GrabbableManager : MonoBehaviour
    {
        public static GrabbableManager Instance;

        // Grabbable Key and Reference
        public const float MAXVELOCITY = 0.25f;
        void Start()
        {
            Instance = this;
            NetworkServer.RegisterHandler<GrabMessage>(ReceivedGrabMessage);
        }

        /// <summary>
        /// This is a grab message from the client sent to the server
        /// </summary>
        public virtual void SendGrabMessage(Grabbable grabbable, Hand hand)
        {
            GrabMessage message = new(grabbable.GetNetworkIdentity(), hand.PhysicsHandCollider.attachedRigidbody.linearVelocity.magnitude);
            NetworkClient.Send(message);
        }

        /// <summary>
        /// This is a grab message from the server received by the client
        /// </summary>
        public virtual void ReceivedGrabMessage(NetworkConnectionToClient connectionId, GrabMessage message)
        {
            if (message.HandVelocity > MAXVELOCITY)
            {
                GiveGrabbableAuthority(connectionId, message.GrabbableId);
            }
        }

        public virtual void GiveGrabbableAuthority(NetworkConnectionToClient connectionId, NetworkIdentity identity) {
            if (identity.connectionToClient != null) identity.RemoveClientAuthority();
            identity.AssignClientAuthority(connectionId);
        }
    }
}
