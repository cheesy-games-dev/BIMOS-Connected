using System.Collections.Generic;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public abstract class SceneProcessor : MonoBehaviour
    {
        public List<NetworkBarcodeT<SceneReference>> LoadedScenes = new();
        public static SceneProcessor singleton { get; set; }
        private void Start() {
            if (singleton && singleton != this)
                return;
            singleton = this;
        }
        public abstract void LoadScene(NetworkBarcodeT<SceneReference> scene);
        public abstract void UnloadScene(NetworkBarcodeT<SceneReference> scene);
    }
}
