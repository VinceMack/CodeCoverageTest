// PawnEditor.cs
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Pawn_VM))]
public class PawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Pawn_VM pawn = (Pawn_VM)target;

        // Display the current Labor Order's type and location
        LaborOrder_Base_VM currentLaborOrder = pawn.GetCurrentLaborOrder();
        if (currentLaborOrder != null)
        {
            EditorGUILayout.LabelField("Current Labor Order Type:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(currentLaborOrder.laborType.ToString());
            EditorGUILayout.LabelField("Current Labor Order Location:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(currentLaborOrder.GetLocation().ToString());
        }
        else
        {
            EditorGUILayout.LabelField("No Current Labor Order", EditorStyles.boldLabel);
        }

        List<LaborType>[] laborTypePriority = pawn.laborTypePriority;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Labor Type Priorities:", EditorStyles.boldLabel);

        for (int i = 0; i < pawn.GetPriorityLevelsCount(); i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Priority Level " + (i + 1), EditorStyles.boldLabel);

            if(laborTypePriority == null){
                EditorGUILayout.LabelField("No Labor Types at this level");
                EditorGUILayout.EndVertical();
                continue;
            }

            if (laborTypePriority[i] != null)
            {
                foreach (var laborType in laborTypePriority[i])
                {
                    EditorGUILayout.LabelField(laborType.ToString());
                }
            }
            else
            {
                EditorGUILayout.LabelField("No Labor Types at this level");
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        // Add buttons to move a random labor type up or down in priority
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Move Random Labor Type Up"))
        {
            LaborType randomLaborType = (LaborType)Random.Range(0, System.Enum.GetValues(typeof(LaborType)).Length);
            pawn.MoveLaborTypeUpPriorityLevel(randomLaborType);
        }

        if (GUILayout.Button("Move Random Labor Type Down"))
        {
            LaborType randomLaborType = (LaborType)Random.Range(0, System.Enum.GetValues(typeof(LaborType)).Length);
            pawn.MoveLaborTypeDownPriorityLevel(randomLaborType);
        }
        EditorGUILayout.EndHorizontal();
    }
}
