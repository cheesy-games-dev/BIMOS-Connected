using System.Collections.Generic;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Transporting;
using KadenZombie8.BIMOS.Rig;
using UnityEngine;

namespace KadenZombie8.BIMOS
{
    public class GrabbableManager : MonoBehaviour
    {
        public static GrabbableManager Instance;

        public readonly List<Grabbable> ActiveGrabbables = new();
        public const float MAXVELOCITY = 0.25f;
        void Start()
        {
            Instance = this;
            InstanceFinder.ServerManager.RegisterBroadcast<GrabMessage>(ReceivedGrabMessage);
        }
        public void RegisterGrabbable(Grabbable arg)
        {
            ActiveGrabbables.Add(arg);
        }
        public void UnregisterGrabbable(Grabbable arg)
        {
            ActiveGrabbables.Remove(arg);
        }
        /// <summary>
        /// This is a grab message from the client sent to the server
        /// </summary>
        public virtual void SendGrabMessage(Grabbable grabbable, Hand hand)
        {
            var index = ActiveGrabbables.IndexOf(grabbable);
            GrabMessage message = new(index, hand.PhysicsHandCollider.attachedRigidbody.linearVelocity.magnitude);
            InstanceFinder.ClientManager.Broadcast(message);
        }

        /// <summary>
        /// This is a grab message from the server received by the client
        /// </summary>
        public virtual void ReceivedGrabMessage(NetworkConnection connectionId, GrabMessage message, Channel channel = Channel.Reliable)
        {
            if (message.HandVelocity > MAXVELOCITY)
            {
                var grabbable = ActiveGrabbables[message.GrabbableIndex];
                GiveGrabbableAuthority(connectionId, grabbable);
            }
        }

        public virtual void GiveGrabbableAuthority(NetworkConnection connectionId, Grabbable grabbable) {
            grabbable.GetComponentInParent<NetworkObject>().GiveOwnership(connectionId);
        }
    }
}
