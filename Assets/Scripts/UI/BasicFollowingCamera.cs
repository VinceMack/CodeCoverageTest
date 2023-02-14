using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollowingCamera : MonoBehaviour
{
    [SerializeField] private GameObject followObject;

    private void Start() 
    {
        if(followObject == null)
        {
            followObject = new GameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y, transform.position.z);
    }
}
