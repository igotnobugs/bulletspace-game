using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject {

    [System.Serializable]
    public class Script {
        public Sprite portait;
        public string name;
        [TextArea(3, 10)] public string sentence;
        public float time;
    }

    public Script[] scripts;
}



[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor {

    SerializedProperty scripts;
    ReorderableList list;

    private void OnEnable() {
        scripts = serializedObject.FindProperty("scripts");
        list = new ReorderableList(serializedObject, scripts, true, true, true, true);
        list.drawElementCallback = DrawListItems;
        list.drawHeaderCallback = DrawHeader;
        list.elementHeight = 95;
    }

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused) {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        var portaitElement = element.FindPropertyRelative("portait");
        var nameElement = element.FindPropertyRelative("name");
        var sentenceElement = element.FindPropertyRelative("sentence");
        var timeElement = element.FindPropertyRelative("time");

        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight),
            portaitElement,
            GUIContent.none
            );

        EditorGUI.LabelField(new Rect(rect.x + 110, rect.y, 40, EditorGUIUtility.singleLineHeight), "Name");
        EditorGUI.PropertyField(
            new Rect(rect.x + 150, rect.y, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight),
            nameElement,
            GUIContent.none
            );


        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, EditorGUIUtility.currentViewWidth, 75),
            sentenceElement,
            GUIContent.none
            );

        EditorGUI.LabelField(new Rect(rect.x, rect.y + 75, 40, EditorGUIUtility.singleLineHeight), "Time");

        EditorGUI.PropertyField(
            new Rect(rect.x + 40, rect.y + 75, 50, EditorGUIUtility.singleLineHeight),
            timeElement,
            GUIContent.none
            );
    }

    private void DrawHeader(Rect rect) {
        string name = "Script";
        EditorGUI.LabelField(rect, name);
    }
}
