using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : MonoBehaviour
{
    enum PlayerClass : ushort
    {
        FIREFIGHTER = 1,
        PARAMEDIC = 2,
        POLICE_OFFICER = 3
    }

    //Attributes
    Vector3 unitPos;
    Vector3 forward;
    int health;
    float speed;
    //List<GameObject> inventory;
    GameObject heldItem;
    int heldItemID;
    GameObject interactionTarget;
    bool isRunning;

    private NavMeshAgent agent;

    //Initialize vars
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        heldItemID = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 3.5f;
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.destination,agent.transform.position) <= 1.0f)
        {
            agent.isStopped = true;
        }
        ApplySpeedModifier();
    }


    public void MoveUnit(Vector3 destination)
    {
        agent.destination = destination;
        agent.isStopped = false;

    }

    public void InteractWithVehicle(GameObject vehicle)
    {
        Debug.Log("Interact with " + vehicle.transform.parent.name);

    }

    public void InteractWithSituation(GameObject situation)
    {
        Debug.Log("Interact with " + situation.transform.root.name);
        if (situation.transform.tag == "Fire")
        {
            Debug.Log(situation.transform.root);
            FireManager fm = situation.transform.root.GetComponent<FireManager>();
            interactionTarget = fm.gameObject;
            Debug.Log(fm.name + heldItemID);
            if (heldItemID == 3001) //Extinguisher
            {
                fm.StopFireSpread();
                Debug.Log("STOPPING SPREAD");
            }

            if (heldItemID == 3002) //Hose
            {
                fm.SetPutOutFire(true);
                Debug.Log("PUTTING OUT FIRE");
            }
        }
        else if(situation.transform.tag == "DamagedVehicle")
        {
            DamagedVehicle dv = situation.transform.root.GetComponent<DamagedVehicle>();
            if (heldItemID == 3003) //Jaws o' life
            {
                dv.RescueCivilian();
                Debug.Log("RESCUING CIVILIAN");
            }
        }
        else if (situation.transform.tag == "Injured")
        {
            InjuredCivilian ic = situation.GetComponent<InjuredCivilian>();
            interactionTarget = ic.gameObject;
            if (heldItemID == 1001) //Medkit
            {
                ic.Heal();
                Debug.Log("Fixing up victim.");
            }
            if (heldItemID == 1002) //Defibulator
            {
                ic.Revive();
                Debug.Log("Clear!, Shocking!");
            }
        }
        else if (situation.transform.tag == "Criminal")
        {
            if (heldItemID == 2001) //Baton
            {

            }

        }
    }

    public void ResetAction()
    {
        if (interactionTarget)
        {
            if (interactionTarget.transform.tag == "Fire")
            {
                interactionTarget.GetComponent<FireManager>().ResumeFire();
                interactionTarget.GetComponent<FireManager>().SetPutOutFire(false);
            }
            if (interactionTarget.transform.tag == "Injured")
            {

            }
        }
    }

    public void PickupItem(GameObject item)
    {
        heldItemID = item.transform.root.gameObject.GetComponent<ItemIdentifier>().ID;
        Debug.Log(item + " picked up" + heldItemID);
        heldItem = item;
        item.transform.root.position = transform.position + Vector3.Normalize(transform.right);
        item.transform.root.rotation = transform.rotation;
        item.transform.root.SetParent(this.transform);
    }

    public float GetDistance(GameObject item)
    {
        float distance = 0.0f;

        distance = Vector3.Distance(transform.position,item.transform.position);

        return distance;
    }

    //I intend to take this project further after class, this will be used to modify speed based on running and caried object weight
    void ApplySpeedModifier()
    {
        if (isRunning)
        {
            speed = 5;
        }
        else
        {
            speed = 3.5f;
        }
        switch (heldItemID)
        {
            case 1001: //Medkit
                speed -= 0.5f;
                break;
            case 1002: //Defibrillator
                speed -= 0.5f;
                break;
            //Police items go here
            case 3001: //Extinguisher
                speed -= 1;
                break;
            case 3002: //Hose
                speed -= 1.5f;
                break;
            default:
                break;
        }

        gameObject.GetComponent<NavMeshAgent>().speed = speed;
    }
}
