using KadenZombie8.BIMOS.Rig;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

namespace KadenZombie8.BIMOS
{
    [DisallowMultipleComponent]
    public partial class BIMOSNetworkManager : NetworkManager
    {
        public static BIMOSNetworkManager Instance;
        [Header("BIMOS")]
        public BIMOSRig DefaultRig;
        public virtual void TryGetDefaults()
        {
            TryGetComponent(out GrabbableManager.Instance);
        }

        void OnEnable()
        {
            Instance = this;
            playerPrefab = DefaultRig.gameObject;
            AddListeners();
        }

        void OnDisable()
        {
            RemoveListeners();
        }
    }
}
