using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DefaultNamespace.ScriptableEvents;
using DefaultNamespace.ScriptableEvents.Editor;
using Variables;

public class AstroEdit : EditorWindow
{
    int mode = 0;
    string[] modeNames = {"Objects", "Edit", "Events", "Graphics"};
    Vector2 scrollPos;

    ScriptableEventBase currentEvent = null;
    ScriptableObject currentObj = null;
    [MenuItem("Tools/AstroEdit")] public static void Open() => GetWindow<AstroEdit>( "Astroid Game General Editor" );


    void OnGUI()
    {
        mode = GUILayout.Toolbar(mode, modeNames);
        
        switch (mode)
        {
            case 1:  ObjMode();   break;
            case 2:  EventMode();   break;
            case 3:  GfxMode();   break;
            case 4:         break;
            default:    break;
        }
    }


    void EventMode()
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
                if (GUILayout.Button( "Create New Event" ,EditorStyles.toolbarButton, GUILayout.Width(200)))
                {
                    currentEvent = null;
                }
            }

            var scriptableEvents = Resources.FindObjectsOfTypeAll<ScriptableEventBase>();

            foreach (ScriptableEventBase obj in scriptableEvents)
            {
                using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
                {
                    using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                    {
                        GUILayout.Label( obj.name );
                    }
                    ScriptableEventEditor.MakeGUI(obj);
                }
            }
        }

        using (new GUILayout.VerticalScope (EditorStyles.helpBox))
        {
            using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
            {
                GUILayout.Label( "Event Variables");

            }
            var eventVars = Resources.FindObjectsOfTypeAll<IntObservable>();
            foreach (IntObservable obj in eventVars)
            {
                using ( new GUILayout.HorizontalScope ( EditorStyles.helpBox ))
                {
                    SerializedObject so = new SerializedObject(obj);
                    so.Update();
                    GUILayout.Label(obj.name);
                    //EditorGUILayout.PropertyField( so.FindProperty( "_value" ));
                    EditorGUILayout.PropertyField( so.FindProperty( "_onValueChangedEvent" ));
                    so.ApplyModifiedProperties();
                }
            }
        }

        using (new GUILayout.VerticalScope( EditorStyles.helpBox ))
        {
            using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
            {
                GUILayout.Label( "Event I/O" );
                if (GUILayout.Button( "Examine" ,EditorStyles.toolbarButton, GUILayout.Width(100)))
                {
                    //currentEvent = null;
                }
            }

            var gameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            using ( var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
            {
                scrollPos = scrollView.scrollPosition;
                //foreach (GameObject go in gameObjects)
                //{
                    var l = FindObjectsOfType<MonoBehaviour>();
                    foreach (var script in l ) //go.GetComponentsInChildren<MonoBehaviour>(true)) 
                    {
                            if (script == null) continue;
                            if (!(script.ToString().Contains("ScriptableEvent"))) continue;
                        
                            
                            SerializedObject so = new SerializedObject(script);
                            so.Update();
                            
                            string targetVariableTypeName = typeof(ScriptableEventInt).Name;
                            //GUILayout.Label(targetVariableTypeName);
                            var property = so.GetIterator();
                            
                            while (property.NextVisible(true))
                            {
                                //GUILayout.Label("" + property.propertyType);
                                //if (property.propertyType != SerializedPropertyType.ObjectReference) continue;
                                //if (!property.type.ToString().Contains("ScriptableEvent")) continue;
                                using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
                                {
                                // We found our property!
                                // Invokes a custom method, which
                                // can validate & do other stuff
                                /*
                                if (property.isArray) //also manages arrays
                                {
                                    for (int i = 0; i < property.arraySize; i++)
                                        OnVariable.Invoke(property.GetArrayElementAtIndex(i));
                                } */
                                using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                                {
                                //GUILayout.Label( "" + property.objectReferenceValue );
                                }
                                EditorGUILayout.PropertyField( property );
                                }
                            }
                            so.ApplyModifiedProperties();
                        
                    }

                //}
            }
        }
    }

    void ObjMode()
    {
        using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
        {
            GUILayout.Label( "Object Properties Editor" , EditorStyles.boldLabel);
            if (GUILayout.Button( "Close" ,EditorStyles.toolbarButton, GUILayout.Width(60)))
                {
                    currentEvent = null;
                }
        }
        

        if (currentEvent != null)
            {
                
                using (new GUILayout.VerticalScope( EditorStyles.helpBox ))
                {
                    GUILayout.Label( "Object Inspector" );
                    ScriptableEventEditor.MakeGUI(currentEvent);
                }
                

                return;
            }
        //List<ScriptableEventBase> scriptableEvents = new List<ScriptableEventBase>();
        var objects = Resources.FindObjectsOfTypeAll<FloatVariable>();
        foreach (FloatVariable obj in objects)
        {
            if (GUILayout.Button( obj.name ))
                {
                    AssetDatabase.OpenAsset(obj.GetInstanceID());
                    currentObj = obj;
                }
        }
    }

    void GfxMode()
    {
        var objects = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
        foreach (SpriteRenderer obj in objects)
        {
            if (GUILayout.Button( obj.name ))
                {
                    AssetDatabase.OpenAsset(obj.GetInstanceID());
                    //currentObj = obj;
                }
        }
    }


}
