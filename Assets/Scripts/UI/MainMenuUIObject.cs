using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIObject : MonoBehaviour
{
    public void MouseOver()
    {
        GetComponent<Image>().enabled = true;
    }

    public void MouseExit()
    {
        GetComponent<Image>().enabled = false;
    }
}
