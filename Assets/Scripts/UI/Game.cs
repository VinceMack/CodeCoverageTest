using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUserInterfaceManager : MonoBehaviour
{
    
    [SerializeField]
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown("p"))
            {
                Debug.Log("P was pressed.");
                if (panel.activeSelf) {
                    panel.SetActive(false);
                }
                else
                {
                    panel.SetActive(true);
                }
            }
    }
}
