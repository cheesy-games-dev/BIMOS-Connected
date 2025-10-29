using UnityEngine;
using KadenZombie8.BIMOS.Rig;
using System;
using FishNet.Object;
using FishNet;
using FishNet.Connection;
using FishNet.Broadcast;
using System.Collections.Generic;
using FishNet.Transporting;

namespace KadenZombie8.BIMOS.Networking {
    [RequireComponent(typeof(Grabbable))]
    public class NetworkGrip : MonoBehaviour {
        private static Dictionary<int, NetworkGrip> gripInstances = new Dictionary<int, NetworkGrip>();
        public int NetId;
        private NetworkObject networkObject;
        private Grabbable grabbable;
        private Rigidbody rb;

        private void Awake() {
            NetId = GetNextId();
            gripInstances.Add(NetId, this);
            rb = GetComponent<Rigidbody>();
            grabbable = GetComponent<Grabbable>();
            networkObject = GetComponentInParent<NetworkObject>();
            grabbable.OnGrab += OnGrabbed;
        }

        private static int GetNextId() {
            int id = 0;
            while (gripInstances.ContainsKey(id)) {
                id++;
            }
            return id;
        }

        private void Start() {
            OnStartServer();
        }

        [Client]
        private void OnGrabbed() {
            SendGripTakeover();
        }

        [Client]
        private void SendGripTakeover() {
            InstanceFinder.ClientManager.Broadcast(new GripTakeover(NetId));
        }

        [Server]
        private void OnStartServer() {
            InstanceFinder.ServerManager.RegisterBroadcast<GripTakeover>(ServerGripTakeover);
            if (networkObject.LocalConnection != null) {
                networkObject.GiveOwnership(networkObject.LocalConnection);
            }
        }

        [Server]
        private static void ServerGripTakeover(NetworkConnection conn, GripTakeover grip, Channel channel) {
            if(channel != Channel.Reliable)
                return;
            var networkObject = gripInstances[grip.netId].networkObject;
            if (networkObject.Owner == conn)
                return;
            networkObject.RemoveOwnership(false);
            networkObject.GiveOwnership(conn);
        }
    }

    public struct GripTakeover : IBroadcast {
        public int netId;
        public GripTakeover(int netId) {
            this.netId = netId;
        }
    }
}