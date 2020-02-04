using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //Attributes
    Vector3 position;
    //this will be forward not relative to the camera, but to the map
    Vector3 forward;

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        //gameObject.transform.position
        forward = Quaternion.Euler(-30, 0, 0) * gameObject.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        gameObject.transform.position = position;
    }

    void Movement()
    {
        //forward
        if (Input.GetKey(KeyCode.W))
        {
            position += forward;
        }
        //left
        else if (Input.GetKey(KeyCode.A))
        {
            position -= (Quaternion.Euler(0,90,0) * forward);
        }
        //backwards
        else if (Input.GetKey(KeyCode.S))
        {
            position -= forward;
        }
        //right
        else if (Input.GetKey(KeyCode.D))
        {
            position += (Quaternion.Euler(0, 90, 0) * forward);
        }
    }

    void Rotation()
    {

    }
}
