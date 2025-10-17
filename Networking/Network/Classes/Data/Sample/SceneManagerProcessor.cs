using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS.Networking
{
    public class SceneManangerProcessor : SceneProcessor
    {
        public override void LoadScene(NetworkBarcodeT<SceneReference> scene) {
            var netbar = NetworkManager.singleton.SceneList.Find(x => scene.barcode == x.barcode);
            var reference = netbar.reference;
            LoadedScenes.Add(netbar);
            SceneManager.LoadSceneAsync(reference.SceneAsset.name, reference.LoadSceneMode);
        }

        public override void UnloadScene(NetworkBarcodeT<SceneReference> scene) {
            var netbar = NetworkManager.singleton.SceneList.Find(x => scene.barcode == x.barcode);
            var reference = netbar.reference;
            LoadedScenes.Remove(netbar);
            SceneManager.UnloadSceneAsync(reference.SceneAsset.name);
        }
    }
}
