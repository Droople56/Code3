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
    float speed;

    Vector3 camLocalEuler;

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        //gameObject.transform.position
        forward = Quaternion.Euler(-30, 0, 0) * gameObject.transform.forward;
        up = Quaternion.Euler(0, 0, 90) * forward;
        speed = .5f;
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
            position += forward * speed;
        }
        //left
        if (Input.GetKey(KeyCode.A))
        {
            position -= (Quaternion.Euler(0,90,0) * forward) * speed;
        }
        //backwards
        if (Input.GetKey(KeyCode.S))
        {
            position -= forward * speed;
        }
        //right
        if (Input.GetKey(KeyCode.D))
        {
            position += (Quaternion.Euler(0, 90, 0) * forward) * speed;
        }
        //Up
        if (Input.GetKey(KeyCode.Equals))
        {
            position += Vector3.up * speed;
        }
        //Down
        if (Input.GetKey(KeyCode.Minus))
        {
            position -= Vector3.up * speed;
        }
        gameObject.transform.position = position;
    }

    void Rotation()
    {
        //Left
        if (Input.GetKey(KeyCode.Q))
        {
            forward = (Quaternion.Euler(0, -3, 0) * forward).normalized;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            forward = (Quaternion.Euler(0, 3, 0) * forward).normalized;
        }
        gameObject.transform.forward = forward;
        gameObject.transform.Rotate(30, 0, 0);
    }
}
