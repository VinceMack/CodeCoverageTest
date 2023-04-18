using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueChange : MonoBehaviour
{
    public Slider mySlider;
    public TMP_InputField myInput;

    public void SliderChange()
    {
        myInput.text = mySlider.value.ToString();
    }

    public void InputChange()
    {
        mySlider.value = int.Parse(myInput.text);
    }
}
