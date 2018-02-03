using UnityEngine;
using System.Text;
using UnityEditor;

public static class EditorUtils
{
    public static readonly GUILayoutOption[] NO_OPTIONS = { };

    public static void Title(string text)
    {
        var titleStyle = new GUIStyle();
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.fontSize = 16;
        GUILayout.Label(text, titleStyle, NO_OPTIONS);
    }

    public static void Label(string text)
    {
        var labelStyle = new GUIStyle();
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.fontSize = 12;
        GUILayout.Label(text, labelStyle, NO_OPTIONS);
    }

    public static void EmptySpace(float height)
    {
        GUILayoutOption[] layout = { GUILayout.Height(height) };
        GUILayout.Label("", layout);
    }

    public static void HorizontalLine(string repeat = "═", string color = "black")
    {
        var style = new GUIStyle();
        style.richText = true;
        EmptySpace(8);
        var line = new StringBuilder(repeat);
        for (int i = 0; i < 7; i++)
            line.Append(line);
        line.Insert(0, ">").Insert(0, color).Insert(0, "<color=").Append("</color>");
        EditorGUILayout.LabelField(line.ToString(), style, NO_OPTIONS);
        EmptySpace(8);
    }
}
