using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Dialog.Replica))]
public class ReplicaPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var actorRect = new Rect(position.x, position.y, position.width, position.height / 5);
        var textRect = new Rect(position.x, position.y + 5, position.width, 3 * position.height / 5);

        EditorGUI.PropertyField(actorRect, property.FindPropertyRelative("actor"), GUIContent.none);
        EditorGUI.PropertyField(textRect, property.FindPropertyRelative("text"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 100;
    }
}
