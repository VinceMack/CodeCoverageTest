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

    public void Initialize(int slot, SaveSystemManager saveManager, SaveSystemUIManager uiManager)
    {
        saveSlot = slot;
        slotName.text = "Save #" + saveSlot;
        myUIManager = uiManager;

        SaveStats myInfo = saveManager.LoadInfo<SaveStats>(slot);
        slotInfo.text = "Last Played: " + myInfo.dateLastPlayed + " " + myInfo.timeLastPlayed;
    }

    public void SelectSlot()
    {
        myUIManager.activeSlot = saveSlot;
    }
}
