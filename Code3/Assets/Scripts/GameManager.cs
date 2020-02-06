using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject selectedObject;
    private GameObject prevSelectedObject;

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;
        prevSelectedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GetObjectClicked();
        }
    }


    
    void GetObjectClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            //If it is the same game object, don't do anything
            if (selectedObject == hit.transform.gameObject)
            {
                return;
            }
            if (selectedObject != null)
            {
                prevSelectedObject = selectedObject;
            }




            selectedObject = hit.transform.gameObject;
        }
    }





}
