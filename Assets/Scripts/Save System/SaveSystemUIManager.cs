using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSystemUIManager : MonoBehaviour
{
    [SerializeField] private SaveSystemManager mySaveManager;
    [SerializeField] private GameObject saveSlot;
    [SerializeField] private GameObject content;

    public int activeSlot = -1;

    public void OpenSaveMenu()
    {
        foreach(Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < Constants.SAVE_SLOT_NUMBER; i++)
        {
            GameObject slot = Instantiate(saveSlot);
            slot.transform.SetParent(content.transform);
            slot.GetComponent<SaveSlot>().Initialize(i, mySaveManager, this);
        }
    }
}
