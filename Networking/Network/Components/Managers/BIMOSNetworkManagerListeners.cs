using Mirror;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS
{
    public partial class BIMOSNetworkManager : NetworkManager {
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
    }
}
