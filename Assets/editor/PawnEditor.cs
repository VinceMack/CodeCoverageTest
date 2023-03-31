using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pawn_VM))]
public class PawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Pawn_VM pawn = (Pawn_VM)target;
        List<LaborType>[] LaborTypePriority;

        if (pawn != null)
        {
            LaborTypePriority = pawn.GetLaborTypePriority();

            if (LaborTypePriority != null)
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
            else
            {
                EditorGUILayout.LabelField("Labor Type Priority not initialized.");
            }
        }


        if(GUILayout.Button("Move Random Labor Type To Random Priority Level")) // button to move a random labor type to a random priority level for testing purposes (this will be used by UI developers)
        {
            // Call the moveLaborTypeToPriorityLevel function with a random labor type and a random priority level
            int type = Random.Range(0, LaborOrderManager_VM.GetNumberOfLaborTypes());
            int priority = Random.Range(0, 4);
            pawn.MoveLaborTypeToPriorityLevel((LaborType)type, priority);
            
            // debug print the moved labor type and the priority level
            Debug.Log("Moved " + (LaborType)type + " to priority level " + priority);
        }
    }
}
