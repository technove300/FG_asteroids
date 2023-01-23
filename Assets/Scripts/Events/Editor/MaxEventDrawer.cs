using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(MaxEvent))]
public class MaxEventEditor : PropertyDrawer
{
    /*
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 100f;
    } */

    Vector2 scrollPos;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        List<SerializedProperty> properties = new List<SerializedProperty>();
        SerializedProperty listProperty = property.FindPropertyRelative("callbacks");
        //EditorGUI.PropertyField(position, listProperty);
        for (int i = 0; i < listProperty.arraySize; i++)
        {
            var element = listProperty.GetArrayElementAtIndex(i);
            properties.Add(element);
        }
        using (new GUILayout.VerticalScope( EditorStyles.helpBox ))
        {
            using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
            {
                GUILayout.Label( property.name , EditorStyles.boldLabel);
            }   
            using ( var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
            {

                scrollPos = scrollView.scrollPosition;
                foreach(SerializedProperty sp in properties)
                {
                    EditorGUILayout.PropertyField(sp, new GUIContent("Callback"));
                }
            }
            using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
            {
                if (GUILayout.Button( " Add Callback " ,EditorStyles.toolbarButton))
                {
                    listProperty.InsertArrayElementAtIndex(listProperty.arraySize);
                }
                if (GUILayout.Button( " Remove Callback " ,EditorStyles.toolbarButton))
                {
                    if (listProperty.arraySize < 1) return;
                    properties.Remove(listProperty.GetArrayElementAtIndex(listProperty.arraySize - 1));
                    listProperty.DeleteArrayElementAtIndex(listProperty.arraySize - 1);
                }
            }
        }
    

        EditorGUI.EndProperty();
    }
}
