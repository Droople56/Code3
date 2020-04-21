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

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.destination,agent.transform.position) <= 1.0f)
        {
            agent.isStopped = true;
        }
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

            if (heldItemID == 3003) //Jaws o' life
            {

            }
        }
        else if (situation.transform.tag == "Injured")
        {
            InjuredCivilian ic = situation.GetComponent<InjuredCivilian>();
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
}
