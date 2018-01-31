using UnityEngine;
using UnityEditor;

public class CustomCreationMenu
{
    [MenuItem("Workshop/Create/Level", false, 100)]
    static void CreateLevel()
    {
        GameObject level = new GameObject("Level - New Level", new []{ typeof(Level) });
        level.layer = LayerMask.NameToLayer("LevelEdition");
        Selection.activeGameObject = level;
        if (SceneView.lastActiveSceneView != null)
            SceneView.lastActiveSceneView.FrameSelected();
    }

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
}
