/*

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SceneController : MonoBehaviour {

    private MenuController mainMenu;

    [SerializeField,Tooltip("Input the index of your room")]
    public int roomIndex;
    [SerializeField] private TextMeshProUGUI lastPlayedText;
    [SerializeField] private Button playButton;
    public SaveSystemManager saveManager;
    public SaveSystemUIManager saveUIManager;

	// Use this for initialization
	void Start () 
    {
        mainMenu = FindObjectOfType<MenuController>();   
	}

    public void SetText()
    {
        if(lastPlayedText != null)
        {
            if(Directory.Exists(Application.persistentDataPath + $"/{roomIndex+1}"))
            {
                SaveStats saveStats = saveManager.LoadInfo<SaveStats>(roomIndex+1);
                lastPlayedText.text = "Last Played:\n" + saveStats.dateLastPlayed + " " + saveStats.timeLastPlayed;
                playButton.gameObject.SetActive(true);
            }
            else
            {
                lastPlayedText.text = "No save detected.";
                playButton.gameObject.SetActive(false);
            }
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (gameObject.transform.GetSiblingIndex() == 0 && !MenuController.instance.backgroundsController.GetComponent<Animation>().isPlaying)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Return();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                NextSlot();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                PreviousSlot();
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                SelectSlot();
            }

        }
	}

    public void NextSlot()
    {
        MenuController.instance.advanceScene();
    }

    public void PreviousSlot()
    {
        MenuController.instance.goBackScene();
    }

    public void Return()
    {
        MenuController.instance.closeScenes();
    }

    public void SelectSlot()
    {
        if(Directory.Exists(Application.persistentDataPath + $"/{roomIndex+1}"))
        {   
            saveUIManager.Load(roomIndex, true, 0);
        }
    }
}


*/ 