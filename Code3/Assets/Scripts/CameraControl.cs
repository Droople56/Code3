using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //Attributes
    Vector3 position;
    Vector3 forward;

    // Start is called before the first frame update
    void Start()
    {
        position = this.transform.position;
        //forward = 
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetButtonDown("w"))
        {
            position += forward;
        }
        else if (Input.GetButtonDown("A"))
        {

        }
        else if (Input.GetButtonDown("S"))
        {

        }
        else if (Input.GetButtonDown("D"))
        {

        }
    }

    void Rotation()
    {

    }
}
