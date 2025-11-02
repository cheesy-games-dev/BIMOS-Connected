using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public class GameObjectVisibility : ObjectVisibility<GameObject> {
        protected override void Disable(VisibleObject @object) {
            @object.obj.SetActive(false);
        }

        protected override void Enable(VisibleObject @object) {
            if (!@object.showOnAuth)
                return;
            @object.obj.SetActive(true);
        }
    }
}
