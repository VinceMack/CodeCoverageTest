using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    private Camera mainCamera;
    private const int CAMERA_BOUNDS = 1000;
    private const int CAMERA_Z_POSITION = -10;
    private const float SPEED = 20.0f;

    public CameraManager(Camera mainCamera)
    {
        this.mainCamera = mainCamera;
    }

    public void UpdateCamera()
    {
        if (Input.GetKey(KeyCode.W)) {
            Vector3 currentPosition = mainCamera.transform.position;
            Vector3 target = new Vector3(currentPosition.x, CAMERA_BOUNDS, CAMERA_Z_POSITION);
            float movementSpeed = SPEED * Time.deltaTime;
            mainCamera.transform.position = Vector3.MoveTowards(currentPosition, target, movementSpeed);
	    }
	    if (Input.GetKey(KeyCode.D)) {
            Vector3 currentPosition = mainCamera.transform.position;
            Vector3 target = new Vector3(CAMERA_BOUNDS, currentPosition.y, CAMERA_Z_POSITION);
            float movementSpeed = SPEED * Time.deltaTime;
            mainCamera.transform.position = Vector3.MoveTowards(currentPosition, target, movementSpeed);
	    }
	    if (Input.GetKey(KeyCode.S)) {
            Vector3 currentPosition = mainCamera.transform.position;
            Vector3 target = new Vector3(currentPosition.x, CAMERA_BOUNDS * -1, CAMERA_Z_POSITION);
            float movementSpeed = SPEED * Time.deltaTime;
            mainCamera.transform.position = Vector3.MoveTowards(currentPosition, target, movementSpeed);
	    } 
	    if (Input.GetKey(KeyCode.A)) {
            Vector3 currentPosition = mainCamera.transform.position;
            Vector3 target = new Vector3(CAMERA_BOUNDS * -1, currentPosition.y, CAMERA_Z_POSITION);
            float movementSpeed = SPEED * Time.deltaTime;
            mainCamera.transform.position = Vector3.MoveTowards(currentPosition, target, movementSpeed);
	    }
    }
}
