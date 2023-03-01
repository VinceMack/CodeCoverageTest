using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private MenuController mainMenu;

    [SerializeField,Tooltip("Input the index of your room")]
    public int roomIndex;

	// Use this for initialization
	void Start () {
        mainMenu = FindObjectOfType<MenuController>();
	}
	
	// Update is called once per frame
	void Update () {
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

    }
}
