﻿using UnityEngine;
using UnityEditor;

public class RoomCreator
{
    [MenuItem("Atelier/Create/Spaceship", false, 100)]
    static void CreateSpaceship()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Spaceships/BaseSpaceship.prefab");
        var copy = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        copy.name = "My Spaceship";
        PrefabUtility.DisconnectPrefabInstance(copy);
        Selection.activeGameObject = copy;
        SceneView.lastActiveSceneView.FrameSelected();
    }
}
