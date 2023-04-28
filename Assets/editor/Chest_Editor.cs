using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Chest))]
public class Chest_Editor : Editor
{
    SerializedProperty contents;
    SerializedProperty location;

    private void OnEnable()
    {
        contents = serializedObject.FindProperty("contents");
        location = serializedObject.FindProperty("location");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        Chest chest = (Chest)target;

        EditorGUILayout.PropertyField(location, new GUIContent("Location"));

        EditorGUILayout.LabelField("Chest Contents", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical(GUI.skin.box);

        if (chest.contents.Count == 0)
        {
            EditorGUILayout.LabelField("Chest is empty");
        }
        else
        {
            foreach (var item in chest.contents)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(item.Key);
                EditorGUILayout.IntField(item.Value);
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}




