using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DefaultNamespace.ScriptableEvents;
using DefaultNamespace.ScriptableEvents.Editor;
using Variables;

public static class EventEdit
{
    static ScriptableEventBase currentEvent = null;
    static Vector2 scrollPos;
    public static void ModalEditor()
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
                    var l = Object.FindObjectsOfType<MonoBehaviour>();
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
}
