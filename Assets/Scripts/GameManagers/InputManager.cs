using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputManager class to handle user input and interaction with the grid
public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // No initialization needed in this case
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Convert the mouse position from screen space to world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Convert the world space mouse position to a grid position
            Vector3Int gridPosition = GridManager.tileMap.WorldToCell(mousePosition);

            // Get the tile at the grid position
            BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(gridPosition);

            // If a tile is found at the position, print its information
            if (tile != null)
            {
                Debug.Log("MOUSE CLICK " + mousePosition + " FOUND: " + tile.returnTileInformation());
            }
            // If no tile is found, print a message indicating the position
            else
            {
                Debug.LogError("No tile found at position: " + mousePosition);
            }
        }
    }
}
