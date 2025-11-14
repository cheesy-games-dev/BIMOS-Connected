#if UNITY_EDITOR
using FishNet.Object.Synchronizing;
using UnityEditor;
using UnityEngine;

public abstract class SyncBasePropertyDrawer : PropertyDrawer {
    public abstract string ClassName {
        get;
    }
    public abstract string PropertyName {
        get;
    }
    GUIContent syncBaseIndicatorContent => new GUIContent(ClassName, $"This variable has been marked with the {ClassName}<>.");

    public override void OnGUI(Rect position, SerializedProperty baseProperty, GUIContent label) {
        var property = baseProperty.FindPropertyRelative(PropertyName);
        Vector2 syncVarIndicatorRect = EditorStyles.miniLabel.CalcSize(syncBaseIndicatorContent);
        float valueWidth = position.width - syncVarIndicatorRect.x;

        Rect valueRect = new Rect(position.x, position.y, valueWidth, position.height);
        Rect labelRect = new Rect(position.x + valueWidth, position.y, syncVarIndicatorRect.x, position.height);

        EditorGUI.PropertyField(valueRect, property, label, true);
        GUI.Label(labelRect, syncBaseIndicatorContent, EditorStyles.miniLabel);
    }

    public override float GetPropertyHeight(SerializedProperty baseProperty, GUIContent label) {
        var property = baseProperty.FindPropertyRelative(PropertyName);
        return EditorGUI.GetPropertyHeight(property);
    }
}

[CustomPropertyDrawer(typeof(SyncVar<>))]
public class SyncVarPropertyDrawer : SyncBasePropertyDrawer {
    public override string ClassName => "SyncVar";

    public override string PropertyName => "_value";
}

[CustomPropertyDrawer(typeof(SyncList<>))]
public class SyncListPropertyDrawer : SyncBasePropertyDrawer {
    public override string ClassName => "SyncList";

    public override string PropertyName => "Collection";
}

/*Found that serialized syncvars look ugly by default for no reason,
just reworked the mirror syncvar editor for fishnet so it looks better and at home
attached a two screenshots for comparison*/

#endif