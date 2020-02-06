using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //Attributes
    Vector3 position;
    //this will be forward not relative to the camera, but to the map
    Vector3 forward;
    Vector3 up;

    Vector3 camLocalEuler;

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        //gameObject.transform.position
        forward = Quaternion.Euler(-30, 0, 0) * gameObject.transform.forward;
        up = Quaternion.Euler(0, 0, 90) * forward;

        camLocalEuler.x = 30;
        gameObject.transform.localEulerAngles = camLocalEuler;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
    }

    void Movement()
    {
        //forward
        if (Input.GetKey(KeyCode.W))
        {
            position += forward;
        }
        //left
        if (Input.GetKey(KeyCode.A))
        {
            position -= (Quaternion.Euler(0,90,0) * forward);
        }
        //backwards
        if (Input.GetKey(KeyCode.S))
        {
            position -= forward;
        }
        //right
        if (Input.GetKey(KeyCode.D))
        {
            position += (Quaternion.Euler(0, 90, 0) * forward);
        }
        //Up
        if (Input.GetKey(KeyCode.Equals))
        {
            position += (Quaternion.Euler(-90, 0, 0) * forward);
        }
        //Down
        if (Input.GetKey(KeyCode.Minus))
        {
            position -= (Quaternion.Euler(-90, 0, 0) * forward);
        }
        gameObject.transform.position = position;
    }

    void Rotation()
    {
        //Left
        if (Input.GetKey(KeyCode.Q))
        {
            forward = (Quaternion.Euler(0, -3, 0) * forward).normalized;
            gameObject.transform.forward = forward;
            gameObject.transform.Rotate(30, 0, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            forward = (Quaternion.Euler(0, 3, 0) * forward).normalized;
            gameObject.transform.forward = forward;
            gameObject.transform.Rotate(30, 0, 0);
        }
    }
}
