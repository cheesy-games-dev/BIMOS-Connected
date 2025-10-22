using System;
using System.Collections.Generic;
using UnityEngine;

namespace HL.Networking
{
    [CreateAssetMenu(fileName = "NetworkSettings", menuName = "HL Networking/NetworkSettings")]
    public class NetworkSettings : ScriptableObject
    {
        public string Author;
        public string App;
        public List<Prefab> Prefabs = new();

        public void OnEnable() {
            Network.Settings = this;
            Prefabs.ForEach(Prefab.GenerateKey);
            Author = Application.companyName;
            App = Application.productName;
        }
    }

    [Serializable]
    public struct Prefab {
        public string key;
        public GameObject value;

        public static void GenerateKey(Prefab prefab) {
            if (string.IsNullOrWhiteSpace(prefab.key))
                return;
            prefab.key = $"{Network.Settings.Author}.{Network.Settings.App}.{prefab.value.name}";
        }
    }

}
