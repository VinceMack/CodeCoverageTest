using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    public float visibleY;
    public float hiddenY;
    public int secondsMovement = 1;
    public float amountPerSecond;
    public int direction = -1;
    public bool changing;
    public float wiggleRoom = .05f;

    public GameObject arrowButton;

    [ContextMenu("GetLocation")]
    public void Getlocation()
    {
        Debug.Log(transform.position);
    }

    [ContextMenu("ChangeMode")]
    public void ChangeMode()
    {
        direction = transform.position.y < hiddenY + wiggleRoom ? 1 : -1;
        amountPerSecond = Mathf.Abs(visibleY - hiddenY) / (float)secondsMovement;
        changing = true;
    }

    private void FixedUpdate() //50 times a second
    {
        if(changing)
        {
            if(transform.position.y < hiddenY - wiggleRoom || transform.position.y > visibleY + wiggleRoom)
            {
                float yPos = direction > 0 ? visibleY : hiddenY;
                transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

                if(arrowButton != null)
                {
                    arrowButton.transform.localRotation = Quaternion.Euler(arrowButton.transform.localRotation.x, arrowButton.transform.localRotation.y, 90 * direction);
                }
                
                changing = false;
                return;
            }
            
            transform.position = new Vector3(transform.position.x, transform.position.y + (amountPerSecond / 50 * direction), transform.position.z);
        }
    }
}
