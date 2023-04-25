using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMovement { UP, LEFT, DOWN, RIGHT, LEVEL_UP, LEVEL_DOWN }

public class CameraManager
{
    private static readonly Camera mainCamera = Camera.main;
    private static readonly int CAMERA_Z_POSITION = 0;
    private static float SPEED = 20.0f;
    private static readonly float DEFAULT_ORTHOGRAPHIC_SIZE = 11.0f;
    private static readonly float ZOOM_IN_LIMIT = 7.0f;
    private static readonly float ZOOM_SPEED = 0.5f;
    private static int currentLevel;

    public static void InitializeCamera()
    {
        float levelXMax = GridManager.mapLevels[0].getXMax();
        float levelYMax = GridManager.mapLevels[0].getYMax();
        float cameraX = levelXMax / 2;
        float cameraY = levelYMax / 2;
        mainCamera.transform.position = new Vector3(cameraX, cameraY, 0);

        // Set initial camera orthographic size
        mainCamera.orthographicSize = DEFAULT_ORTHOGRAPHIC_SIZE;
    }

    private static int getCurrentLevel()
    {
        return (int)mainCamera.transform.position.x / GridManager.LEVEL_WIDTH;
    }

    private static Vector3 getNewPosition(CameraMovement input)
    {
        Vector3 currentPosition = mainCamera.transform.position;
        float movementSpeed = SPEED * Time.deltaTime;
        float targetX, targetY;
        switch (input)
        {
            case CameraMovement.UP:     // W
                targetX = currentPosition.x;
                targetY = GridManager.mapLevels[currentLevel].getYMax();
                break;

            case CameraMovement.RIGHT:  // D
                targetX = GridManager.mapLevels[currentLevel].getXMax();
                targetY = currentPosition.y;
                break;

            case CameraMovement.DOWN:   // S
                targetX = currentPosition.x;
                targetY = GridManager.mapLevels[currentLevel].getYMin();
                break;

            case CameraMovement.LEFT:   // A
                targetX = GridManager.mapLevels[currentLevel].getXMin();
                targetY = currentPosition.y;
                break;

            default:
                return new Vector3(currentPosition.x, currentPosition.y, CAMERA_Z_POSITION);
        }

        Vector3 target = new Vector3(targetX, targetY, CAMERA_Z_POSITION);
        return Vector3.MoveTowards(currentPosition, target, movementSpeed);
    }

    private static void MoveCamera(CameraMovement input)
    {
        currentLevel = getCurrentLevel();
        float height = mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;
        Vector3 newPosition;

        switch (input)
        {
            case CameraMovement.UP:     // W
                newPosition = getNewPosition(input);
                int yMax = GridManager.mapLevels[currentLevel].getYMax();
                if (newPosition.y + height < yMax)
                {
                    mainCamera.transform.position = newPosition;
                }
                else
                {
                    float correction = newPosition.y + height - yMax;
                    newPosition.y -= correction;
                    mainCamera.transform.position = newPosition;
                }
                break;

            case CameraMovement.RIGHT:  // D
                newPosition = getNewPosition(input);
                int xMax = GridManager.mapLevels[currentLevel].getXMax();
                if (newPosition.x + width < xMax)
                {
                    mainCamera.transform.position = newPosition;
                }
                else
                {
                    float correction = newPosition.x + width - xMax;
                    newPosition.x -= correction;
                    mainCamera.transform.position = newPosition;
                }
                break;

            case CameraMovement.DOWN:   // S
                newPosition = getNewPosition(input);
                int yMin = GridManager.mapLevels[currentLevel].getYMin();
                if (newPosition.y - height > yMin)
                {
                    mainCamera.transform.position = newPosition;
                }
                else
                {
                    float correction = newPosition.y - height - yMin;
                    newPosition.y += (-1 * correction);
                    mainCamera.transform.position = newPosition;
                }
                break;

            case CameraMovement.LEFT:   // A
                newPosition = getNewPosition(input);
                int xMin = GridManager.mapLevels[currentLevel].getXMin();
                if (newPosition.x - width > xMin)
                {
                    mainCamera.transform.position = newPosition;
                }
                else
                {
                    float correction = newPosition.x - width - xMin;
                    newPosition.x += (-1 * correction);
                    mainCamera.transform.position = newPosition;
                }
                break;

            case CameraMovement.LEVEL_UP:
                if (currentLevel != 0)
                {
                    float newCameraX = mainCamera.transform.position.x - GridManager.LEVEL_WIDTH;
                    float newCameraY = mainCamera.transform.position.y;
                    mainCamera.transform.position = new Vector3(newCameraX, newCameraY, 0);
                }
                break;

            case CameraMovement.LEVEL_DOWN:
                if (GridManager.mapLevels.Count - 1 > currentLevel)
                {
                    float newCameraX = mainCamera.transform.position.x + GridManager.LEVEL_WIDTH;
                    float newCameraY = mainCamera.transform.position.y;
                    mainCamera.transform.position = new Vector3(newCameraX, newCameraY, 0);
                }
                break;
        }
    }

    private static void UpdateCameraOrthographicSize()
    {
        currentLevel = getCurrentLevel();
        float delta = Input.mouseScrollDelta.y;
        float newHeight = mainCamera.orthographicSize + (delta * ZOOM_SPEED); // NEW
        float newWidth = newHeight * mainCamera.aspect;

        float cameraX = mainCamera.transform.position.x;
        float cameraY = mainCamera.transform.position.y;
        float xAdjustment = 0, yAdjustment = 0;

        // Prevent zoom from exceeding level size or zoom in limit
        if (newHeight * 2.0f < GridManager.LEVEL_HEIGHT - 1 &&
            newWidth * 2.0f < GridManager.LEVEL_WIDTH - 1 &&
            newHeight > ZOOM_IN_LIMIT)
        {
            // Set new camera position if zooming out at edge of level
            if (cameraX + newWidth > GridManager.mapLevels[currentLevel].getXMax())
            {
                xAdjustment = (cameraX + newWidth) - GridManager.mapLevels[currentLevel].getXMax();
            }
            else if (cameraX - newWidth < GridManager.mapLevels[currentLevel].getXMin())
            {
                xAdjustment = (cameraX - newWidth) - GridManager.mapLevels[currentLevel].getXMin();
            }

            if (cameraY + newHeight > GridManager.mapLevels[currentLevel].getYMax())
            {
                yAdjustment = (cameraY + newHeight) - GridManager.mapLevels[currentLevel].getYMax();
            }
            else if (cameraY - newHeight < GridManager.mapLevels[currentLevel].getYMin())
            {
                yAdjustment = (cameraY - newHeight) - GridManager.mapLevels[currentLevel].getYMin();
            }

            mainCamera.orthographicSize = newHeight;
            mainCamera.transform.position = new Vector3(cameraX - xAdjustment, cameraY - yAdjustment, CAMERA_Z_POSITION);
        }
    }

    public static void UpdateCamera()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W)) // Move camera up
            {
                MoveCamera(CameraMovement.UP);
            }

            if (Input.GetKey(KeyCode.D)) // Move camera right
            {
                MoveCamera(CameraMovement.RIGHT);
            }

            if (Input.GetKey(KeyCode.S)) // Move camera down
            {
                MoveCamera(CameraMovement.DOWN);
            }

            if (Input.GetKey(KeyCode.A)) // Move camera left
            {
                MoveCamera(CameraMovement.LEFT);
            }

            if (Input.GetKeyDown(KeyCode.Q)) // Move camera up 1 level
            {
                MoveCamera(CameraMovement.LEVEL_UP);
            }

            if (Input.GetKeyDown(KeyCode.E)) // Move camera down 1 level
            {
                MoveCamera(CameraMovement.LEVEL_DOWN);
            }
        }

        if (Input.mouseScrollDelta.y != 0) // Camera Zoom
        {
            UpdateCameraOrthographicSize();
        }
    }
}
