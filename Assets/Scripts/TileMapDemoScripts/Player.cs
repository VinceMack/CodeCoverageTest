using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    private List<Vector3> path;

    private void Awake()
    {
        path = new List<Vector3>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void updateLocation(float speed)
    {
        if(path.Count > 0){
            transform.position = Vector3.MoveTowards(transform.position, path.Last(), speed);
            if (transform.position == path.Last()) path.RemoveAt(path.Count - 1);
	    }
    }

    public void updatePath(List<Vector3> newPath)
    {
        path.Clear();
        path = newPath;
    }
}
