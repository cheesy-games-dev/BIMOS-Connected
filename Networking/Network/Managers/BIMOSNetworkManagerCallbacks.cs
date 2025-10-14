using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS
{
    public partial class BIMOSNetworkManager : MonoBehaviour
    {
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
