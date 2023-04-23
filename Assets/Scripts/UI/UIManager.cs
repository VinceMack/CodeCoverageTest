using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIManager
{
    public enum UIMode { normal = 1, normalFoodFarm = 2, premiumFoodFarm = 3, clothFarm = 4, normalMedFarm = 5, premiumMedFarm = 6, destroy = 7, create = 8 };

    public static UIMode myMode;

    public static Texture2D farmCursor = Resources.Load("Cursors/plant") as Texture2D;
    public static Texture2D destroyCursor = Resources.Load("Cursors/break") as Texture2D;
    public static Texture2D createCursor = Resources.Load("Cursors/build") as Texture2D;
    public static CursorMode cursorMode = CursorMode.Auto;
    public static Vector2 hotSpot = Vector2.zero;

    public static void SelectUIMode(int newMode)
    {
        myMode = (UIMode)newMode;

        switch(newMode)
        {
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            {
                GameObject.Find("Canvas/Warnings/CancelHint").SetActive(true);
                GameObject.Find("Canvas/Warnings/DragDropInfo").SetActive(true);
                Cursor.SetCursor(farmCursor, hotSpot, cursorMode);
                break;
            }
            case 7:
            {
                GameObject.Find("Canvas/Warnings/CancelHint").SetActive(true);
                GameObject.Find("Canvas/Warnings/DragDropInfo").SetActive(true);
                Cursor.SetCursor(destroyCursor, hotSpot, cursorMode);
                break;
            }
            case 8:
            {
                GameObject.Find("Canvas/Warnings/CancelHint").SetActive(true);
                GameObject.Find("Canvas/Warnings/DragDropInfo").SetActive(true);
                Cursor.SetCursor(createCursor, hotSpot, cursorMode);
                break;
            }
            default:
            {
                GameObject.Find("Canvas/Warnings/CancelHint").SetActive(false);
                GameObject.Find("Canvas/Warnings/DragDropInfo").SetActive(false);
                Cursor.SetCursor(null, hotSpot, cursorMode);
                break;
            }
        }
    }

    public static void ToggleMode(int newMode)
    {
        if(myMode == (UIMode)newMode)
        {
            SelectUIMode((int)UIMode.normal);
        }
        else
        {
            SelectUIMode(newMode);
        }
    }
}
