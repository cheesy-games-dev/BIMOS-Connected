using KadenZombie8.BIMOS.Rig;
using Mirror;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

namespace KadenZombie8.BIMOS.Networking
{
    public class NETMOSRig : NetworkBehaviour
    {
        public BIMOSRig rig;
        private void Start() {
            TryGetComponent(out rig);
            int layer = LayerMask.GetMask("Player", "Rig", "BIMOSRig", "BIPEDRig");
            Physics.IgnoreLayerCollision(layer, layer, true);
            if (this.IsOwner()) {
                BIMOSRig.Instance = rig;
                BIMOSLogger.Log($"{rig.name} is Local Rig");
            }
            else {
                BIMOSLogger.Log($"{rig.name} is Online Rig");
                Destroy(rig.GetComponentInChildren<EventSystem>().gameObject);
                Destroy(rig.GetComponentInChildren<SettingsMenu>().gameObject);
                var blacklist = rig.GetComponentsInChildren<Camera>().Cast<Object>().ToList();
                blacklist.AddRange(rig.GetComponentsInChildren<TrackedPoseDriver>());
                foreach (var item in blacklist)
                    Destroy(item);
            }
        }
    }
}
