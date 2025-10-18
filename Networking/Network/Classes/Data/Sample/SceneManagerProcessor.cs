using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS.Networking
{
    public class SceneManangerProcessor : SceneProcessor
    {
        public new static SceneManangerProcessor singleton;
        public override void InitSingleton() {
            singleton = this;
        }

        public override async void LoadSceneAsync(NetworkBarcodeT<SceneReference> scene) {
            var netbar = SceneList.Find(x => scene.barcode == x.barcode);
            var reference = netbar.reference;
            LoadedScenes.Add(reference);
            await SceneManager.LoadSceneAsync(reference.SceneKey, reference.LoadSceneMode);
            OnSceneLoaded?.Invoke(reference);
        }

        public override void UnloadSceneAsync(NetworkBarcodeT<SceneReferenceT<SceneAsset>> scene) {
            var netbar = SceneList.Find(x => scene.barcode == x.barcode);
            var reference = netbar.reference;
            LoadedScenes.Remove(netbar);
            SceneManager.UnloadSceneAsync(reference.SceneKey.name);
            OnSceneUnloaded?.Invoke(reference);
        }
    }
}
