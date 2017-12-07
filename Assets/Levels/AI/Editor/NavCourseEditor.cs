using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavCourse))]
public class NavCourseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NavCourse course = (NavCourse)target;
        if (GUILayout.Button("Add Nav Point"))
        {
            course.AddNavPoint();
        }
    }
}
