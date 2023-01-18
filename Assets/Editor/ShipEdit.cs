using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Variables;
using Ship;

public static class ShipEdit
{
    static Vector2 scrollPos;
    public static void ModalEditor()
    {
        
        //List<ScriptableEventBase> scriptableEvents = new List<ScriptableEventBase>();
        var objects = Resources.FindObjectsOfTypeAll<ShipConfig>();
        foreach (ShipConfig obj in objects)
        {

            using ( new GUILayout.VerticalScope ( EditorStyles.helpBox ))
            {
                SerializedObject so = new SerializedObject(obj);
                so.Update();
                GUILayout.Label(AssetDatabase.GetAssetPath(obj.GetInstanceID()));
                EditorGUILayout.PropertyField( so.FindProperty( "throttlePower" ));
                EditorGUILayout.PropertyField( so.FindProperty( "rotationPower" ));
                EditorGUILayout.PropertyField( so.FindProperty( "laserSpeed" ));
                //EditorGUILayout.PropertyField( so.FindProperty( "_value" ));
                so.ApplyModifiedProperties();
            }

        }

        List<String> checkedObjs = new List<String>();
        SerializedObject so_Laser = null, so_Ship = null;
        SerializedProperty shipColor = null, laserColor = null, shipSprite = null, laserSprite = null;
        Color ref_shipColor = Color.white, ref_laserColor = Color.white;
        Sprite ref_shipSprite = null, ref_laserSprite = null; 
        using ( var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
        {
            scrollPos = scrollView.scrollPosition;
            var renderers = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
            foreach (SpriteRenderer obj in renderers)
            {
                if (checkedObjs.Contains(obj.name))     continue;
                checkedObjs.Add(obj.name);
                
                
                if (obj.name.Contains("Ship_Sprite"))
                {
                    so_Ship = new SerializedObject(obj);
                    shipColor = so_Ship.FindProperty("m_Color");
                    shipSprite = so_Ship.FindProperty("m_Sprite");
                    ref_shipColor = shipColor.colorValue;
                    ref_shipSprite = shipSprite.objectReferenceValue as Sprite;
                    continue;
                }

                if (obj.name.Contains("Laser_Sprite"))
                {
                    so_Laser = new SerializedObject(obj);
                    laserColor = so_Laser.FindProperty("m_Color");
                    laserSprite = so_Laser.FindProperty("m_Sprite");
                    ref_laserColor = laserColor.colorValue;
                    ref_laserSprite = laserSprite.objectReferenceValue as Sprite;
                    continue;
                }
            }


            using (new EditorGUILayout.HorizontalScope())
            {
                var centeredLabel = GUI.skin.GetStyle("Label");
                centeredLabel.alignment = TextAnchor.MiddleCenter;

                using (new EditorGUILayout.VerticalScope())
                {
                    GUI.color = ref_laserColor;
                    GUILayout.Label(ref_laserSprite.texture, GUILayout.MaxHeight(50));
                    GUI.color = ref_shipColor;
                    GUILayout.Label(ref_shipSprite.texture, GUILayout.MaxHeight(100));
                    GUI.color = Color.white;
                }

                centeredLabel.alignment = TextAnchor.UpperLeft;

                using (new EditorGUILayout.VerticalScope( EditorStyles.helpBox ))
                {
                    using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                    {
                        GUILayout.Label("Laser Graphics");
                    }
                    so_Laser.Update();
                    EditorGUILayout.PropertyField( laserColor );
                    EditorGUILayout.PropertyField( laserSprite );
                    so_Laser.ApplyModifiedProperties();
                    using (new GUILayout.HorizontalScope( EditorStyles.toolbar))
                    {
                        GUILayout.Label("Ship Graphics");
                    }
                    so_Ship.Update();
                    EditorGUILayout.PropertyField( shipColor );
                    EditorGUILayout.PropertyField( shipSprite );
                    so_Ship.ApplyModifiedProperties();
                }
            }
        }
    }
}
