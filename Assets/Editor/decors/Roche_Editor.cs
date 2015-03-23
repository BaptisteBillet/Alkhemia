using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Roche_Alea_Tool))]
public class Roche_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Roche_Alea_Tool myTarget = (Roche_Alea_Tool)target;


        if (GUILayout.Button("Construire une roche aléatoire"))
        {
            myTarget.BuildObject();
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