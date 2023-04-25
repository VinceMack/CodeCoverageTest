/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;

    [SerializeField] private GameObject saveSystemUI;

    [SerializeField] private GameObject colonyInfo;
    [SerializeField] private ColonyInfoManager colonyInfoManager;

    public void CloseSaveSystemUI()
    {
       saveSystemUI.SetActive(false);
    }

    public void EnableSaveSystemUI()
    {
        CloseMenus();
        saveSystemUI.SetActive(true);
        saveSystemUI.GetComponent<SaveSystemUIManager>().OpenSaveMenu();
    }

    public void ToggleSaveSystemUI()
    {
        if(saveSystemUI.activeSelf)
        {
            CloseSaveSystemUI();
        }
        else
        {
            EnableSaveSystemUI();
        }
    }

    public void CloseColonyUI()
    {
       colonyInfo.SetActive(false);
    }

    public void EnableColonyUI()
    {
        CloseMenus();
        colonyInfo.SetActive(true);
        colonyInfoManager.OpenColonyInfo();
    }

    public void ToggleColonyUI()
    {
        if(colonyInfo.activeSelf)
        {
            CloseColonyUI();
        }
        else
        {
            EnableColonyUI();
        }
    }

    private void CloseMenus()
    {
        CloseSaveSystemUI();
        CloseColonyUI();
    }
}


*/