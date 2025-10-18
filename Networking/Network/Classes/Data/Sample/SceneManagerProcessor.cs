using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS.Networking
{
    public class SceneManangerProcessor : SceneProcessorT<SceneReferenceT<SceneAsset>>
    {
        public new static SceneManangerProcessor singleton;
        public override void InitSingleton() {
            singleton = this;
        }

        public override async void LoadSceneAsync(SceneReferenceT<SceneAsset> reference) {
            var netbar = SceneList.Find(x => reference == x.reference);
            LoadedScenes.Add(reference);
            await SceneManager.LoadSceneAsync(reference.sceneKey.name, reference.LoadSceneMode);
            OnSceneLoaded?.Invoke(reference);
        }

        public override async void UnloadSceneAsync(SceneReferenceT<SceneAsset> reference) {
            var netbar = SceneList.Find(x => reference == x.reference);
            LoadedScenes.Remove(reference);
            await SceneManager.UnloadSceneAsync(reference.sceneKey.name);
            OnSceneUnloaded?.Invoke(reference);
        }
    }
}
