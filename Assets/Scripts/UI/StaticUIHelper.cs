using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticUIHelper : MonoBehaviour
{
    // These non-static methods are necessary to be able to call their namesake static methods because
    // Unity for some reason does not support calling static functions via Button components.

    public void ToggleMode(int newMode)
    {
        UIManager.ToggleMode(newMode);
    }

    public void SelectUIMode(int newMode)
    {
        UIManager.SelectUIMode(newMode);
    }
}
