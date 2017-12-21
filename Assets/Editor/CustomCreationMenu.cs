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

    [MenuItem("Workshop/Create/NavGraph", false, 101)]
    static void CreaseNavCourse()
    {
        GameObject graph = new GameObject("NavGraphStart", new []{ typeof(NavNode) });
        Selection.activeGameObject = graph;
        if (SceneView.lastActiveSceneView != null)
            SceneView.lastActiveSceneView.FrameSelected();
    }
}
