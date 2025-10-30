using Riptide;
using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ServerCallback : Attribute
    {
        public bool OnlyServer {
            get; private set;
        }
        public ServerCallback(bool onlyServer = false) {
            OnlyServer = onlyServer;
        }
    }
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ClientCallback : Attribute {
        public bool OnlyClient {
            get; private set;
        }
        public ClientCallback(bool onlyClient = false) {
            OnlyClient = onlyClient;
        }
    }
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Rpc : Attribute {
        public Target RpcTarget {
            get; private set;
        }
        public MessageSendMode RpcSendMode {
            get; private set;
        }
        public Rpc(Target Target = Target.All, MessageSendMode SendMode = MessageSendMode.Reliable) {
            RpcTarget = Target;
            RpcSendMode = SendMode;
        }
        /// <summary>
        /// Tasks:
        /// - Add buffered later
        /// </summary>
        public enum Target {
            All,
            Server,
            Clients,
            Others,
        }
    }
}
