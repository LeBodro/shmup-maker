﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavNode))]
public class NavNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NavNode graph = (NavNode)target;
        if (GUILayout.Button("Add Nav Point"))
        {
            NavNode node = graph.InsertNode();
            Selection.activeGameObject = node.gameObject;
            SceneView.lastActiveSceneView.FrameSelected();
        }
    }
}
