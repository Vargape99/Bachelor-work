using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

//to make it possible to choose from colors, instead of having to type them every time
[CustomEditor(typeof(SetColor))]
[CanEditMultipleObjects]
public class SetColorEditor : Editor
{
    SerializedProperty colorProperty;
    string[] colors = new[] { "Red", "Blue", "Yellow", "Orange", "Purple", "Green", "White" };
    int choice = 0;


    private void OnEnable()
    {
        colorProperty = serializedObject.FindProperty("myColor");
        choice = Array.IndexOf(colors, colorProperty.stringValue);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        choice = EditorGUILayout.Popup("Color", choice, colors);
        if (choice < 0) {
            choice = 0;
        }
        colorProperty.stringValue = colors[choice];
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
