using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DefaultNamespace.ScriptableEvents;
using DefaultNamespace.ScriptableEvents.Editor;
using Variables;

public static class AstroidEdit
{
    static Vector2 scrollPos;
    public static void ModalEditor()
    {
        var objects = Resources.FindObjectsOfTypeAll<SpawnerSettings>();
        foreach (SpawnerSettings obj in objects)
        {
            using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
            {
                GUILayout.Label("Asteroid Configuration Editor");
            }
            using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
            {
                using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                {
                GUILayout.Label(obj.name);
                }
                SerializedObject so = new SerializedObject(obj);
                so.Update();
                //GUILayout.Label(obj.name);
                //EditorGUILayout.PropertyField( so.FindProperty( "_value" ));
                EditorGUILayout.PropertyField( so.FindProperty( "_minSpawnTime" ));
                EditorGUILayout.PropertyField( so.FindProperty( "_maxSpawnTime" ));
                EditorGUILayout.PropertyField( so.FindProperty( "_minAmount" ));
                EditorGUILayout.PropertyField( so.FindProperty( "_maxAmount" ));
                so.ApplyModifiedProperties();
            }

        }
    }
}
