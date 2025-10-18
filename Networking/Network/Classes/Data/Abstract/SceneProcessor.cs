using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KadenZombie8.BIMOS.Networking
{
    public abstract class SceneProcessor : MonoBehaviour
    {
        public static SceneProcessor singleton;

        protected virtual void Start() {
            singleton = this;
            InitSingleton();
        }
        public abstract void InitSingleton();
        public abstract void LoadSceneAsync(SceneReference scene);
        public abstract void UnloadSceneAsync(SceneReference scene);
    }

    public abstract class SceneProcessorT<T> : SceneProcessor where T : SceneReference {
        public UnityEvent<T> OnSceneLoaded = new();
        public UnityEvent<T> OnSceneUnloaded = new();
        public List<NetworkBarcodeT<T>> SceneList = new();
        public List<T> LoadedScenes = new();
        public override void LoadSceneAsync(SceneReference scene) => LoadSceneAsync((T)scene);
        public override void UnloadSceneAsync(SceneReference scene) => UnloadSceneAsync((T)scene);
        public abstract void LoadSceneAsync(T scene);
        public abstract void UnloadSceneAsync(T scene);
    }
}
