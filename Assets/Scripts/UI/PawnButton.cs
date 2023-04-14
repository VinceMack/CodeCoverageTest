using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using UnityEngine.UI;
using TMPro;

public class PawnButton : MonoBehaviour, IPointerClickHandler
{

    public Pawn_VM pawn;
    public LaborType labor;
    public GameObject textObj;

    private TMP_Text _text;

    // Pawn button initialization.
    public void InitializePawnButton()
    {
        _text = textObj.GetComponent<TMP_Text>();
        _text.text = pawn.GetPriorityLevelOfLabor(labor).ToString();;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            pawn.MoveLaborTypeUpPriorityLevel(labor);
            _text.text = pawn.GetPriorityLevelOfLabor(labor).ToString();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            pawn.MoveLaborTypeDownPriorityLevel(labor);
            _text.text = pawn.GetPriorityLevelOfLabor(labor).ToString();
        }
    }

}
