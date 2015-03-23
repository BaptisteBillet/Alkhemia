using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Herbe_Alea_Tool))]
public class Herbe_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Herbe_Alea_Tool myTarget = (Herbe_Alea_Tool)target;


        if (GUILayout.Button("Construire une herbe aléatoire"))
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