using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Variables;

public static class VarEdit
{
    static Vector2 scrollPos;
    public static void ModalEditor()
    {
        //List<ScriptableEventBase> scriptableEvents = new List<ScriptableEventBase>();
        var objects = Resources.FindObjectsOfTypeAll<FloatVariable>();
        foreach (FloatVariable obj in objects)
        {

            using ( new GUILayout.HorizontalScope ( EditorStyles.helpBox ))
            {
                SerializedObject so = new SerializedObject(obj);
                so.Update();
                GUILayout.Label(obj.name);
                //EditorGUILayout.PropertyField( so.FindProperty( "_value" ));
                EditorGUILayout.PropertyField( so.FindProperty( "_value" ));
                so.ApplyModifiedProperties();
            }

        }
        var objects1 = Resources.FindObjectsOfTypeAll<IntVariable>();
        foreach (IntVariable obj in objects1)
        {

            using ( new GUILayout.HorizontalScope ( EditorStyles.helpBox ))
            {
                SerializedObject so = new SerializedObject(obj);
                so.Update();
                GUILayout.Label(obj.name);
                //EditorGUILayout.PropertyField( so.FindProperty( "_value" ));
                EditorGUILayout.PropertyField( so.FindProperty( "_value" ));
                so.ApplyModifiedProperties();
            }

        }
    }
}
