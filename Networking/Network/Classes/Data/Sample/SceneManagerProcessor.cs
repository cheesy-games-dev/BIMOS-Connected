using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS.Networking
{
    public class SceneManangerProcessor : SceneProcessorT<SceneAsset>
    {
        public new static SceneManangerProcessor singleton;
        public override void InitSingleton() {
            singleton = this;
        }

        public override async void LoadSceneAsync(SceneAsset scene) {
            var netbar = SceneList.Find(x => scene == x.reference);
            var reference = netbar.reference;
            LoadedScenes.Add(reference);
            await SceneManager.LoadSceneAsync(reference.name, reference.);
            OnSceneLoaded?.Invoke(reference);
        }

        public override void UnloadSceneAsync(SceneAsset scene) {
            var netbar = SceneList.Find(x => scene.barcode == x.barcode);
            var reference = netbar.reference;
            LoadedScenes.Remove(netbar);
            SceneManager.UnloadSceneAsync(reference.SceneKey.name);
            OnSceneUnloaded?.Invoke(reference);
        }
    }
}
