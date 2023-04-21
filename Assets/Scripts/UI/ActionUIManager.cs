using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUIManager : MonoBehaviour
{
    [SerializeField] private List<UIButton> buttons = new List<UIButton>();

    public void DeSelectAll()
    {
        foreach(UIButton button in buttons)
        {
            button.OriginalColor();
        }
    }
}
