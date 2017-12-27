﻿using UnityEditor;
using UnityEngine;
using System.Text;
using System;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    const string LEVEL_PREFIX = "Level - ";
    const string NAV_GRAPH = "NavGraphStart";

    Level level;
    GUILayoutOption[] noOptions = { };
    string assetPath = "";

    public override void OnInspectorGUI()
    {
        level = (Level)target;

        EmptySpace();
        Title("Level Editing");
        Label("Name");
        if (!level.name.Contains(LEVEL_PREFIX))
            level.name = string.Format("{0}{1}", LEVEL_PREFIX, level.name);
        level.name = string.Format("{0}{1}", LEVEL_PREFIX, GUILayout.TextField(level.name.Split(new []{ LEVEL_PREFIX }, System.StringSplitOptions.None)[1], noOptions));

        EmptySpace(4);
        if (GUILayout.Button("Add Nav Graph"))
            CreateNavGraph();
        if (GUILayout.Button("Save Level"))
            Save();
        if (GUILayout.Button("Save and Export Level"))
            SaveAndExport();

        HorizontalLine();
        Title("Dialogs");
        DialogOption("Intro");
        DialogOption("Boss");
        DialogOption("Outro");

        HorizontalLine();
        WaveSection();

        HorizontalLine();
        BossSection();
    }

    void CreateNavGraph()
    {
        GameObject graph = new GameObject(NAV_GRAPH, new []{ typeof(NavNode) });
        graph.transform.SetParent(level.transform);
        Selection.activeGameObject = graph;
        if (SceneView.lastActiveSceneView != null)
            SceneView.lastActiveSceneView.FrameSelected();
    }

    void Save()
    {
        assetPath = string.Format("Assets/Levels/{0}.prefab", level.name);
        PrefabUtility.CreatePrefab(assetPath, level.gameObject);
    }

    void Title(string text)
    {
        var titleStyle = new GUIStyle();
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.fontSize = 16;
        GUILayout.Label(text, titleStyle, noOptions);
    }

    void Label(string text)
    {
        var labelStyle = new GUIStyle();
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.fontSize = 12;
        GUILayout.Label(text, labelStyle, noOptions);
    }

    void EmptySpace(float height = 16)
    {
        GUILayoutOption[] layout = { GUILayout.Height(height) };
        GUILayout.Label("", layout);
    }

    void DialogOption(string type)
    {
        var intro = serializedObject.FindProperty(string.Format("{0}Dialog", type.ToLower())).objectReferenceValue;
        if (intro == null && GUILayout.Button(string.Format("Create {0}", type)))
            CreateDialog(type);
        else if (intro != null && GUILayout.Button(string.Format("Select {0}", type)))
            Selection.activeObject = intro;
    }

    void CreateDialog(string prefix)
    {
        var dialog = ScriptableObject.CreateInstance<Dialog>();
        AssetDatabase.CreateAsset(dialog, string.Format("Assets/Dialogs/{0} - {1}.asset", prefix, level.name));
        serializedObject.FindProperty(string.Format("{0}Dialog", prefix.ToLower())).objectReferenceValue = dialog;
        serializedObject.ApplyModifiedProperties();
        Selection.activeObject = dialog;
    }

    void WaveSection()
    {
        SerializedProperty waves = serializedObject.FindProperty("waves");
        int waveCount = waves.arraySize;
        for (int i = 0; i < waveCount; i++)
        {
            var wave = waves.GetArrayElementAtIndex(i);
            Title(string.Format("Wave {0}", i + 1));
            SerializedProperty formations = wave.FindPropertyRelative("formations");
            int formationCount = formations.arraySize;
            for (int j = 0; j < formationCount; j++)
            {
                Label(string.Format("Formation {0}", j + 1));
                var formation = formations.GetArrayElementAtIndex(j);
                Formation(formation);

                if (GUILayout.Button(string.Format("Remove Formation {0}", j + 1), noOptions))
                    level.RemoveFormation(i, j);

                HorizontalLine("─", "grey");
            }
            if (GUILayout.Button(string.Format("Add Formation to Wave {0}", i + 1), noOptions))
                formations.arraySize++;
            if (GUILayout.Button(string.Format("Remove Wave {0}", i + 1), noOptions))
                level.RemoveWave(i);

            HorizontalLine("▬");
        }
        if (GUILayout.Button("Add Wave", noOptions))
            waves.arraySize++;

        serializedObject.ApplyModifiedProperties();
    }

    void BossSection()
    {
        Title("Boss");
        Formation(serializedObject.FindProperty("boss"));
    }

    void Formation(SerializedProperty formation)
    {
        EditorGUILayout.ObjectField(formation.FindPropertyRelative("graphStart"), noOptions);
        EditorGUILayout.ObjectField(formation.FindPropertyRelative("shipPrefab"), noOptions);
        EditorGUILayout.DelayedIntField(formation.FindPropertyRelative("shipCount"), noOptions);
        EditorGUILayout.DelayedFloatField(formation.FindPropertyRelative("spawnCooldown"), noOptions);
    }

    void HorizontalLine(string repeat = "═", string color = "black")
    {
        var style = new GUIStyle();
        style.richText = true;
        EmptySpace(8);
        var line = new StringBuilder(repeat);
        for (int i = 0; i < 7; i++)
            line.Append(line);
        line.Insert(0, ">").Insert(0, color).Insert(0, "<color=").Append("</color>");
        EditorGUILayout.LabelField(line.ToString(), style, noOptions);
        EmptySpace(8);
    }

    void SaveAndExport()
    {
        Save();
        string exportPath = string.Format("{0} - {1}.unitypackage", level.name, DateTime.UtcNow.Ticks);
        AssetDatabase.ExportPackage(assetPath, exportPath);
    }
}
