using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS
{
    [DisallowMultipleComponent]
    public partial class BIMOSNetworkManager : MonoBehaviour
    {
        public static BIMOSNetworkManager Instance;

        public virtual void TryGetDefaults()
        {
            TryGetComponent(out GrabbableManager.Instance);
        }

        void OnEnable()
        {
            Instance = this;
            AddListeners();
        }

        void OnDisable()
        {
            RemoveListeners();
        }
    }
}
