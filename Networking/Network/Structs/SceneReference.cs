using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS.Networking
{
    [Serializable]
    public struct SceneReference
    {
        public object SceneKey {
            get;set;
        }
        public LoadSceneMode LoadSceneMode { get; set; }
        public SceneAsset SceneAsset;
    }

    public class SceneAsset : UnityEngine.Object {
        
    }
}
