using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DefaultNamespace.ScriptableEvents;
using DefaultNamespace.ScriptableEvents.Editor;
using Variables;

public class EventEdit : EditorWindow
{
    ScriptableEventBase currentEvent = null;
    Vector2 scrollPos;
    string nname = "sadasd";

    [MenuItem("Tools/Event Editor")] public static void Open() => GetWindow<EventEdit>( "Modular Event Editor" );
    public void OnGUI()
    {
        using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
        {
            GUILayout.Label( "Scriptable Event Manager" , EditorStyles.boldLabel);
            if (GUILayout.Button( "Close" ,EditorStyles.toolbarButton, GUILayout.Width(60)))
            {
                currentEvent = null;
            }
        }
        

        if (currentEvent != null)
            {
                
                using (new GUILayout.VerticalScope( EditorStyles.helpBox ))
                {
                    GUILayout.Label( "Event Inspector" );
                    ScriptableEventEditor.MakeGUI(currentEvent);
                }
                

                return;
            }
        //List<ScriptableEventBase> scriptableEvents = new List<ScriptableEventBase>();
        
        
        using (new GUILayout.VerticalScope( EditorStyles.helpBox))
        {
            using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
            {
                GUILayout.Label( "Events" );
                
            }

            var scriptableEvents = Resources.LoadAll<MaxEventObj>("Resources/Scriptable Objects/Events");
            var objects = Resources.FindObjectsOfTypeAll<MaxEventObj>();

            foreach (MaxEventObj obj in objects)
            {
                using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
                {
                    using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                    {
                        GUILayout.Label( obj.name );

                    }
                }
            }
        }
        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope( EditorStyles.helpBox))
            {
                using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                {
                    GUILayout.Label( "Event Listeners" );
                    if (GUILayout.Button( "Add New" ,EditorStyles.toolbarButton))
                    {
                        MaxEventObj newObj = ScriptableObject.CreateInstance<MaxEventObj>();
                        AssetDatabase.CreateAsset(newObj, "Assets/Scriptable Objects/Events/NewEvent.asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }

                var objects = Resources.FindObjectsOfTypeAll<MaxEventObj>();
                foreach (MaxEventObj obj in objects)
                {
                    using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
                    {
                        using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                        {
                        
                        var path = AssetDatabase.GetAssetPath(obj);
                        GUILayout.Label(path);

                        }
                        SerializedObject so = new SerializedObject(obj);
                        so.Update();
                        //GUILayout.Label(obj.name);
                        EditorGUILayout.PropertyField( so.FindProperty( "m_Name"));
                        EditorGUILayout.PropertyField( so.FindProperty( "mevent" ));
                        so.ApplyModifiedProperties();
                        if (!AssetDatabase.GetAssetPath(obj).Contains( so.FindProperty("m_Name").stringValue))
                        {
                            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), so.FindProperty("m_Name").stringValue + ".asset");
                        }
                        if (GUILayout.Button( "Delete" ,EditorStyles.toolbarButton))
                        {
                            AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(obj));
                        }
                    }

                }
            }
            using (new GUILayout.VerticalScope( EditorStyles.helpBox, GUILayout.MaxWidth(500)))
            {
                using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                {
                    GUILayout.Label( "Event Callers" );
                    
                }
                using ( var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
                {
                    scrollPos = scrollView.scrollPosition;
                    var objects = Resources.FindObjectsOfTypeAll<MonoBehaviour>();
                    foreach (MonoBehaviour obj in objects)
                    {
                        List<SerializedProperty> properties = new List<SerializedProperty>();
                        SerializedObject so = new SerializedObject(obj);
                        
                        var property = so.GetIterator();
                        while (property.NextVisible(true))
                        {
                            if (property.type != typeof(MaxEvent).Name) continue;
                            properties.Add(property.Copy());
                        }
                        //Debug.Log(properties.Count);
                        if (properties.Count == 0) continue;
                        so.Update();
                        using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
                        {
                            using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                            {
                                GUILayout.Label(obj.name, EditorStyles.boldLabel);
                                
                                if (GUILayout.Button( "Object Path: " + AssetDatabase.GetAssetOrScenePath(obj) ,EditorStyles.toolbarButton))
                                {
                                    AssetDatabase.OpenAsset(obj);
                                }
                            }
                            foreach (SerializedProperty sp in properties)
                            {
                                using ( new GUILayout.VerticalScope ( ))
                                {
                                    EditorGUILayout.PropertyField(sp);
                                }
                            } 
                        }
                        so.ApplyModifiedProperties();
                    }
                }
            }
        }
    }
}
