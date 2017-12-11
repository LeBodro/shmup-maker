using UnityEngine;
using UnityEditor;

public class CustomCreationMenu
{
    [MenuItem("Workshop/Create/Spaceship", false, 100)]
    static void CreateSpaceship()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Spaceships/BaseSpaceship.prefab");
        var copy = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        copy.name = "My Spaceship";
        PrefabUtility.DisconnectPrefabInstance(copy);
        Selection.activeGameObject = copy;
        if (SceneView.lastActiveSceneView != null)
            SceneView.lastActiveSceneView.FrameSelected();
    }

    [MenuItem("Workshop/Create/NavCourse", false, 101)]
    static void CreaseNavCourse()
    {
        GameObject course = new GameObject("NavCourse", new System.Type[]{ typeof(NavCourse) });
        Selection.activeGameObject = course;
        if (SceneView.lastActiveSceneView != null)
            SceneView.lastActiveSceneView.FrameSelected();
    }
}
