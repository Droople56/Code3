using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedObject;
    [SerializeField]
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
        if (Input.GetMouseButtonDown(0))
        {
            GetObjectClicked();
            Debug.Log("Click");
        }
        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform.gameObject.tag == "Ground")
                {
                    Vector3 dest = new Vector3(hit.point.x, selectedObject.transform.position.y, hit.point.z);
                    selectedObject.transform.GetComponent<PlayerUnit>().MoveUnit(dest);

                }
                else if (hit.transform.gameObject.tag == "Vehicle")
                {
                    selectedObject.transform.GetComponent<PlayerUnit>().InteractWithVehicle(hit.transform.gameObject);
                }
                else if(hit.transform.gameObject.tag == "Item")
                {
                    Vector3 dest = new Vector3(hit.point.x, selectedObject.transform.position.y, hit.point.z);
                    selectedObject.transform.GetComponent<PlayerUnit>().PickupItem(hit.transform.gameObject);
                }
                else
                {
                    Debug.Log("INVALID LOCATION");
                    Debug.Log(hit.transform.gameObject.name);
                    Debug.Log(hit.transform.gameObject.tag);
                }
            }
        }
    }


    
    void GetObjectClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            if (hit.transform.gameObject.GetComponent<TerrainCollider>() != null)
            {
                selectedObject = null;
            }
            else if (selectedObject != null)
            {
                prevSelectedObject = selectedObject;
                selectedObject = hit.transform.gameObject;
            }
            else if(selectedObject == null)
            {
                selectedObject = hit.transform.gameObject;
            }
            
        }
    }





}
