using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(BulletController))]
public class ControllerCustomEditor : Editor
{
    int index = 0;
    public override void OnInspectorGUI()
    {
       
        base.OnInspectorGUI();
        var BulletController = target as BulletController;
        /*
       Rect r = EditorGUILayout.BeginHorizontal();
       index = EditorGUILayout.Popup("Bullet Type",
            index, BulletController.bulletOptions, EditorStyles.popup);
       BulletController.bulletType = BulletController.bulletOptions[index];
       EditorGUILayout.EndHorizontal();
       */
    }
}
