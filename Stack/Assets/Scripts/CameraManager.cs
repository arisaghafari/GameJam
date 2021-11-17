using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{

    void Start()
    {
        transform.position = new Vector3(2.73f, 4.1f, 3.11f);
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.0875f, transform.position.z);
            //transform.position.y + 0.0875f
        }
        //        transform.position = new Vector3(transform.position.x, transform.position.y + MovingCube.CurrentCube.transform.position.y, transform.position.z);   
    }
}
