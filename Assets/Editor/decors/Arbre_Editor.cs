using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Arbre_Alea_Tool))]
public class Arbre_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Arbre_Alea_Tool myScript = (Arbre_Alea_Tool)target;

        if (GUILayout.Button("Construire un arbre aléatoire"))
        {
            myScript.BuildObject();
        }

       
        //EditorGUILayout.
    }
}
/*
        EditorGUILayout.HelpBox("Info", MessageType.Info);
        EditorGUILayout.HelpBox("None",MessageType.None );
        EditorGUILayout.HelpBox("Warning", MessageType.Warning);
        EditorGUILayout.HelpBox("Error", MessageType.Error);
        */