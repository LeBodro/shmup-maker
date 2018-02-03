using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    GameController controller;

    public override void OnInspectorGUI()
    {
        controller = (GameController)target;

        Title("Levels");
        SerializedProperty levels = serializedObject.FindProperty("levels");
        int levelCount = levels.arraySize;
        for (int i = 0; i < levelCount; i++)
        {
            EditorUtils.Label(string.Format("Level {0}", i + 1));
            var level = levels.GetArrayElementAtIndex(i);
            GUIStyle smallWidth = new GUIStyle();
            EditorGUILayout.PropertyField(level, EditorUtils.NO_OPTIONS);
            if (i > 0 && GUILayout.Button("▲", EditorUtils.NO_OPTIONS))
            {
                controller.UpLevel(i);
            }
        }

        DrawDefaultInspector();
    }

    void Title(string text)
    {
        EditorUtils.Title(text);
    }
}
