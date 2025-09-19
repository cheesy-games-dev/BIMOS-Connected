using System;
using FishNet;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS
{
    public partial class BIMOSNetworkManager : MonoBehaviour
    {
        public static Action<BIMOSNetworkManager, Scene, LoadSceneMode> OnLevelLoaded;
        public static Action<BIMOSNetworkManager, Scene> OnLevelUnloaded;
        private void AddListeners()
        {
            SceneManager.sceneLoaded += Manager_SceneLoad;
            SceneManager.sceneUnloaded += Manager_SceneUnload;
        }

        private void RemoveListeners()
        {
            SceneManager.sceneLoaded -= Manager_SceneLoad;
            SceneManager.sceneUnloaded -= Manager_SceneUnload;
        }
        private void Manager_SceneUnload(Scene arg0)
        {
            OnLevelUnloaded?.Invoke(this, arg0);
        }

        private void Manager_SceneLoad(Scene arg0, LoadSceneMode arg1)
        {
            OnLevelLoaded?.Invoke(this, arg0, arg1);
        }
    }
}
