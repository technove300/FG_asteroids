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
    string[] modeNames = {"Ship", "Asteroids", "Variables", "Graphics"};
    Vector2 scrollPos;


    [MenuItem("Tools/AstroEdit")] public static void Open() => GetWindow<AstroEdit>( "Astroid Game General Editor" );


    void OnGUI()
    {
        mode = GUILayout.Toolbar(mode, modeNames);
        
        switch (mode)
        {
            case 0:  ShipEdit.ModalEditor();   break;
            case 1:  AstroidEdit.ModalEditor();   break;
            case 2:  VarEdit.ModalEditor();   break;
            case 3:  GfxEdit.ModalEditor();   break;
            default:    break;
        }
    }

}
