using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;

    [SerializeField] private GameObject saveSystemUI;

    public void CloseSaveSystemUI()
    {
        saveSystemUI.SetActive(false);
    }

    public void EnableSaveSystemUI()
    {
        saveSystemUI.SetActive(true);
    }

    public void ToggleSaveSystemUI()
    {
        saveSystemUI.SetActive(!saveSystemUI.activeSelf);
    }

    private void Update() 
    {
        if (Input.GetKeyDown("e"))
        {
            ToggleSaveSystemUI();
        }
    }
}
