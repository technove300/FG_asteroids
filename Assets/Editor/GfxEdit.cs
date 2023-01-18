using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DefaultNamespace.ScriptableEvents;
using DefaultNamespace.ScriptableEvents.Editor;
using Variables;

public static class GfxEdit
{
    static Vector2 scrollPos;
    public static void ModalEditor()
    {
        List<String> checkedObjs = new List<String>();
        using ( var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
        {
            scrollPos = scrollView.scrollPosition;
            var objects = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
            foreach (SpriteRenderer obj in objects)
            {
                if (checkedObjs.Contains(obj.name))     continue;
                checkedObjs.Add(obj.name);
                
                using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
                {
                    SerializedObject so = new SerializedObject(obj);
                    var sprite = so.FindProperty( "m_Sprite" );
                    var colour = so.FindProperty( "m_Color" );
                    using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                    {
                        GUILayout.Label(obj.name);
                    }
                    using (new GUILayout.HorizontalScope())
                    {
                        so.Update();
                        Sprite sprite_tex = sprite.objectReferenceValue as Sprite;
                        Color color_col = colour.colorValue;
                        
                        using (new GUILayout.VerticalScope( EditorStyles.helpBox, GUILayout.MaxWidth(100) ))
                        {
                            GUI.color = color_col;
                            GUILayout.Label(sprite_tex.texture, GUILayout.MaxHeight(100));
                            GUI.color = Color.white;
                        }
                        
                        using (new GUILayout.VerticalScope( EditorStyles.helpBox, GUILayout.MaxWidth(400)))
                        {
                            EditorGUILayout.PropertyField(  sprite );
                            EditorGUILayout.PropertyField( colour );

                        }
                        
                        /*
                        var property = so.GetIterator();           
                        while (property.NextVisible(true))
                        {
                            using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
                            {
                                GUILayout.Label(property.name);
                                EditorGUILayout.PropertyField( property );
                            }
                        } */
                        so.ApplyModifiedProperties();
                    }
                    
                }
            }
        }
    }
}
