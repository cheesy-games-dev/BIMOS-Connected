using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS
{
    public partial class BIMOSNetworkManager : MonoBehaviour
    {
        public static BIMOSNetworkManager Instance;
        public static List<Type> DefaultComponents = new(){typeof(BIMOSNetworkManager)};
        static BIMOSNetworkManager()
        {
            if (Instance) return;
            var manager = new GameObject("BIMOSNetworkManager");
            DontDestroyOnLoad(manager);
        }
        public virtual void AddDefaultComponents()
        {
            DefaultComponents.Add(typeof(GrabbableManager));
            foreach (var component in DefaultComponents)
            {
                gameObject.AddComponent(component);
            }
            TryGetDefaults();
        }

        public virtual void TryGetDefaults()
        {
            TryGetComponent(out GrabbableManager.Instance);
        }

        void OnEnable()
        {
            Instance = this;
            AddDefaultComponents();
            AddListeners();
        }

        void OnDisable()
        {
            RemoveListeners();
        }
    }
}
