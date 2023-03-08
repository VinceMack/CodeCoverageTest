using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pawn))]
public class PawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Pawn pawn = (Pawn)target;
        List<LaborType>[] LaborTypePriority = pawn.getLaborTypePriority();

        if (pawn != null && LaborTypePriority != null)
        {
            EditorGUILayout.LabelField("Labor Type Priority:"); // display the labor type priority array of lists

            for (int i = 0; i < LaborTypePriority.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("List " + i + ":");
                for (int j = 0; j < LaborTypePriority[i].Count; j++)
                {
                    LaborTypePriority[i][j] = (LaborType)EditorGUILayout.EnumPopup(LaborTypePriority[i][j]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        if(GUILayout.Button("Move Labor Type To Priority Level")) // button to move a random labor type to a random priority level for testing purposes (this will be used by UI developers)
        {
            // Call the moveLaborTypeToPriorityLevel function with a random labor type and a random priority level
            int type = Random.Range(0, LaborOrderManager.getNumberOfLaborTypes());
            int priority = Random.Range(0, 4);
            pawn.moveLaborTypeToPriorityLevel((LaborType)type, priority);
            
            // debug print the moved labor type and the priority level
            Debug.Log("Moved " + (LaborType)type + " to priority level " + priority);
        }
    }
}
