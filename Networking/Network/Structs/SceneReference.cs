using System;
using UnityEngine.SceneManagement;

namespace KadenZombie8.BIMOS.Networking
{
    [Serializable]
    public abstract class SceneReference
    {
        public abstract object SceneKey {
            get;set;
        }
        public LoadSceneMode LoadSceneMode;
    }

    [Serializable]
    public class SceneReferenceT<T> : SceneReference {
        public T sceneKey;

        public override object SceneKey {
            get => sceneKey;
            set => sceneKey = (T)value;
        }
    }
}
