using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;

    [SerializeField] private GameObject colonyInfo;
    [SerializeField] private ColonyInfoManager colonyInfoManager;
    [SerializeField] private GameObject laborOrderPanel;

    private bool initialized = false;

    public void ToggleLaborOrderPanel()
    {
        if (laborOrderPanel.activeSelf)
        {
            laborOrderPanel.SetActive(false);
        }
        else
        {
            laborOrderPanel.SetActive(true);
            if (!initialized)
            {
                initialized = true;
                laborOrderPanel.GetComponentsInChildren<LaborOrderPanelManager>()[0].InitializeLaborOrderPanel();
            }
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
        if (colonyInfo.activeSelf)
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
        CloseColonyUI();
    }
}



