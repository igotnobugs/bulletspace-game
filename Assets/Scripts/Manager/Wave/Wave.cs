using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject
{
    public int spawnPoints = 2;

    [System.Serializable]
    public struct Line {

        [System.Serializable]
        public struct Spawn {
            public EnemyShip enemyShip;
            public int spawnPosition;
        }

        public Spawn[] spawns;
    }

    public Line[] lines;
}


[CustomEditor(typeof(Wave))]
public class WaveEditor : Editor {
    SerializedProperty spawnPoints;
    SerializedProperty lines;

    ReorderableList list;

    private void OnEnable() {
        spawnPoints = serializedObject.FindProperty("spawnPoints");
        

        lines = serializedObject.FindProperty("lines");
        list = new ReorderableList(serializedObject, lines, true, true, true, true);
        list.drawElementCallback = DrawListItems;
        list.drawHeaderCallback = DrawHeader;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused) {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(
            new Rect(rect.x + 5, rect.y, 50, 20),
            element.FindPropertyRelative("spawns"),
            GUIContent.none
            );
    }

    private void DrawHeader(Rect rect) {
        string name = "Line";
        EditorGUI.LabelField(rect, name);
    }
}
