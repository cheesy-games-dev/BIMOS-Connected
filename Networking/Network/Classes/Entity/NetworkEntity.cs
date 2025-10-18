using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking {
    [Serializable]
    public struct NetworkEntity {
        public static List<NetworkEntity> Entities = new();
        public string NetBarcode { get; internal set; }
        public byte NetId { get; internal set; }
        public GameObject NetInstance {
            get; internal set;
        }
        public string barcode;
        public Connection owner;
        public readonly bool IsMine => owner == LayerProcessor.singleton.Client.Connection;
    }
}
