using FishNet.Component.Transforming;
using FishNet.Object;
using KadenZombie8.BIMOS;
using KadenZombie8.BIMOS.Rig;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CustomEditorForRig
{
    [CustomEditor(typeof(BIMOSRig))]
    public class BIMOSRigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var rig = (BIMOSRig)target;
            if (GUILayout.Button("Convert To Networked Rig"))
            {
                BIMOSRigPatch.ConvertToNetworkRig(rig);
                EditorUtility.SetDirty(rig.gameObject);
            }
        }
    }
}
