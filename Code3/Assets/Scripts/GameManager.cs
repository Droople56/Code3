using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedObject;
    [SerializeField]
    private GameObject prevSelectedObject;
    GameObject HUD;
    List<GameObject> paramedics;
    List<GameObject> firefighters;
    List<GameObject> policeOfficers;
    public List<GameObject> playerUnits;

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;
        prevSelectedObject = null;
        HUD = GameObject.Find("HUD");
        paramedics = new List<GameObject>();
        firefighters = new List<GameObject>();
        policeOfficers = new List<GameObject>();
        playerUnits = new List<GameObject>();
        GetPlayerUnits();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetObjectClicked();
            Debug.Log("Click " + selectedObject.transform.name);
        }
        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            selectedObject.transform.GetComponent<PlayerUnit>().ResetAction();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform.gameObject.tag == "Ground")
                {
                    Vector3 dest = new Vector3(hit.point.x, selectedObject.transform.position.y, hit.point.z);
                    selectedObject.transform.GetComponent<PlayerUnit>().MoveUnit(dest);

                }
                else if (hit.transform.tag == "Fire" || hit.transform.tag == "Injured" || hit.transform.tag == "Criminal" || hit.transform.tag == "DamagedVehicle")
                {
                    Vector3 dest = new Vector3(hit.point.x, selectedObject.transform.position.y, hit.point.z);
                    selectedObject.transform.GetComponent<PlayerUnit>().MoveUnit(dest);
                    if (selectedObject.transform.GetComponent<PlayerUnit>().GetDistance(hit.transform.gameObject) < 3.0f)
                    {
                        selectedObject.transform.GetComponent<PlayerUnit>().InteractWithSituation(hit.transform.gameObject);
                    }
                }
                /*else if (hit.transform.parent.tag == "Vehicle")
                {
                    selectedObject.transform.GetComponent<PlayerUnit>().InteractWithVehicle(hit.transform.gameObject);
                }*/
                else if(hit.transform.tag == "Item")
                {
                    Vector3 dest = new Vector3(hit.point.x, selectedObject.transform.position.y, hit.point.z);
                    selectedObject.transform.GetComponent<PlayerUnit>().MoveUnit(dest);
                    if (selectedObject.transform.GetComponent<PlayerUnit>().GetDistance(hit.transform.gameObject) < 3.0f)
                    {
                        selectedObject.transform.GetComponent<PlayerUnit>().PickupItem(hit.transform.gameObject);
                    }
                }
                else
                {
                    Debug.Log("INVALID CONTEXT");
                    Debug.Log(hit.transform.gameObject.name);
                    Debug.Log(hit.transform.tag);
                }
            }
        }
        UpdateUI();
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

    void UpdateUI()
    {
        string updatedList = "UNITS \n";
        foreach (var unit in playerUnits)
        {
            updatedList += " " + unit.transform.name + "\n";
        }
        HUD.transform.GetChild(0).gameObject.GetComponent<Text>().text = updatedList;
    }

    void GetParamedics()
    {
        GameObject medicList = GameObject.Find("Paramedics");
        for (int i = 0; i < medicList.transform.childCount; i++)
        {
            paramedics.Add(medicList.transform.GetChild(i).gameObject);
        }
    }

    void GetFireFighters()
    {
        GameObject fireList = GameObject.Find("Firefighters");
        for (int i = 0; i < fireList.transform.childCount; i++)
        {
            firefighters.Add(fireList.transform.GetChild(i).gameObject);
        }
    }

    void GetPoliceOfficers()
    {
        GameObject copList = GameObject.Find("Police");
        for (int i = 0; i < copList.transform.childCount; i++)
        {
            policeOfficers.Add(copList.transform.GetChild(i).gameObject);
        }
    }

    void GetPlayerUnits()
    {
        GetParamedics();
        GetFireFighters();
        GetPoliceOfficers();
        playerUnits.AddRange(paramedics);
        playerUnits.AddRange(firefighters);
        playerUnits.AddRange(policeOfficers);
    }

}
