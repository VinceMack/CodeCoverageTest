using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;

    [SerializeField] private GameObject saveSystemUI;

    public void CloseSaveSystemUI()
    {
       //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
       saveSystemUI.SetActive(false);
    }

    public void EnableSaveSystemUI()
    {
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

    private void Update() 
    {
        if (Input.GetKeyDown("e"))
        {
            ToggleSaveSystemUI();
        }
    }
}
