using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class CustomCreationMenu
{
    [MenuItem("Workshop/Create/Spaceship", false, 100)]
    static void CreateSpaceship()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Spaceships/BaseSpaceship.prefab");
        GameObject copy = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        copy.name = "My Spaceship";
        PrefabUtility.DisconnectPrefabInstance(copy);

        Selection.activeGameObject = copy;
        if (SceneView.lastActiveSceneView != null)
            SceneView.lastActiveSceneView.FrameSelected();
    }

    [MenuItem("Workshop/Create/NavCourse", false, 101)]
    static void CreaseNavCourse()
    {
        GameObject course = new GameObject("NavCourse", new []{ typeof(NavCourse) });
        Selection.activeGameObject = course;
        if (SceneView.lastActiveSceneView != null)
            SceneView.lastActiveSceneView.FrameSelected();
    }
}
