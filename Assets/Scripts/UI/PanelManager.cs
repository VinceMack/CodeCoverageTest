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
        colonyInfo.GetComponent<ColonyInfoManager>().OpenColonyInfo();
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

    private void Update() 
    {
        if (Input.GetKeyDown("e"))
        {
            ToggleSaveSystemUI();
        }

        if(Input.GetKeyDown("i"))
        {
            ToggleColonyUI();
        }
    }

    private void CloseMenus()
    {
        CloseSaveSystemUI();
        CloseColonyUI();
    }
}
