using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScoreTally))]
public class ScoreTallyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ScoreTally targetScript = (ScoreTally)target;
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        EditorGUI.EndChangeCheck();

        if (GUI.changed)
        {
            targetScript.SetCurrentLevel();
        }
    }
}
