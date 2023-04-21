using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Color originalColor;
    [SerializeField] protected Color selectedColor;
    [SerializeField] protected List<UIButton> comparableButtons = new List<UIButton>();

    private void Awake() 
    {
        originalColor = GetComponent<Image>().color;
    }

    public virtual void Select()
    {
        if(GetComponent<Image>().color == originalColor)
        {
            foreach(UIButton button in comparableButtons)
            {
                if(button.GetComponent<Image>().color != button.originalColor)
                {
                    button.GetComponent<Button>().onClick.Invoke();
                }
            }
            SpecialColor();
        }
        else
        {
            OriginalColor();
        }
    }

    public void SpecialColor()
    {
        GetComponent<Image>().color = selectedColor;
    }

    public void OriginalColor()
    {
        GetComponent<Image>().color = originalColor;
    }
}
