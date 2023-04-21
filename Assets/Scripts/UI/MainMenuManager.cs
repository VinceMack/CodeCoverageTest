using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public static MainMenuManager instance;

    // Animations
    private bool isAnimating = false;

    // Sounds
    private AudioSource Audio;

    // Home Screen
    [SerializeField, HideInInspector]
    public GameObject homeScreen;

    // Menu Options
    [SerializeField, HideInInspector]
    public GameObject newMenu;
    [SerializeField, HideInInspector]
    public GameObject loadMenu;
    [SerializeField, HideInInspector]
    public GameObject settingsMenu;
    [SerializeField, HideInInspector]
    public GameObject exitMenu;

    // Start is called before the first frame update
    void Start()
    {
        Audio = gameObject.GetComponent<AudioSource>();
        instance = this;

        // PLAY ANIMATION HERE
        returnHome();

    }

    // Update is called once per frame
    void Update()
    {
        if (!homeScreen.activeSelf && !isAnimating)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                returnHome();
            }
        }
    }

    // Disable all menus within main menu
    public void disableMenus()
    {
        homeScreen.SetActive(false);
        newMenu.SetActive(false);
        loadMenu.SetActive(false);
        settingsMenu.SetActive(false);
        exitMenu.SetActive(false);
    }

    // Return to home screen
    public void returnHome()
    {
        disableMenus();
        homeScreen.SetActive(true);
    }

    // Manages the selected option from home screen
    public void selectOption(string option)
    {
        if (homeScreen.activeSelf && !isAnimating)
        {
            disableMenus();
            switch(option)
            {
                case "new":
                    newMenu.SetActive(true);
                    //PlayGame();
                    // PLAY ANIMATION HERE
                    break;
                case "load":
                    loadMenu.SetActive(true);
                    //LoadSaveGame();
                    // PLAY ANIMATION HERE
                    break;
                case "settings":
                    settingsMenu.SetActive(true);
                    // PLAY ANIMATION HERE
                    break;
                case "exit":
                    exitMenu.SetActive(true);
                    // PLAY ANIMATION HERE
                    break;
                default:
                    homeScreen.SetActive(true);
                    break;
            }
        }
    }

    public void PlayGame(){
        SceneManager.LoadScene(1);
    }
    
    public void LoadSaveGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

}
