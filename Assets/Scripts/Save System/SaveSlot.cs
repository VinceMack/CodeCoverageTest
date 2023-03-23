using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private SaveSystemUIManager myUIManager;

    [SerializeField] private TextMeshProUGUI slotName;
    [SerializeField] private TextMeshProUGUI slotInfo;
    [SerializeField] private int saveSlot;

    public void Initialize(int slot, ISaveSystemManager saveManager, SaveSystemUIManager uiManager, bool saveDetected)
    {
        saveSlot = slot;
        slotName.text = "Save #" + saveSlot;
        myUIManager = uiManager;

        if(!saveDetected)
        {
            slotInfo.text = "No save detected.";
        }
        else
        {
            SaveStats myInfo = saveManager.LoadInfo<SaveStats>(slot);
            slotInfo.text = "Last Played: " + myInfo.dateLastPlayed + " " + myInfo.timeLastPlayed;
        }
        
    }

    public void SelectSlot()
    {
        myUIManager.activeSlot = saveSlot;
    }

    public string GetSaveInfoText()
    {
        return slotInfo.text;
    }

    public void SetTexts(TextMeshProUGUI newSlotName, TextMeshProUGUI newSlotInfo)
    {
        slotName = newSlotName;
        slotInfo = newSlotInfo;
    }
}
