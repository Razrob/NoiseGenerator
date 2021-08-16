using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextureSaver))]
public class TextureSaverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TextureSaver _textureSaver = target as TextureSaver;
        base.OnInspectorGUI();
        GUILayout.Space(20);
        if (GUILayout.Button("Generate and save texture", GUILayout.Height(40))) _textureSaver.Generate();
    }
}
