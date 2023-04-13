using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Check for user input
    public static void CheckForInput()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = GridManager.tileMap.WorldToCell(mousePosition);
            BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(gridPosition);

            if (tile != null)
            {
                Debug.Log("MOUSE CLICK " + mousePosition + " FOUND: " + tile.ToString());
            }
            else
            {
                Debug.Log("No tile found at position: " + mousePosition);
            }
        }

        if(Input.GetKey("delete"))
        {
            UIManager.SelectUIMode(1);
        }
    }
}
