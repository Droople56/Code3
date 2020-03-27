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

    private NavMeshAgent agent;

    //Initialize vars
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
        Debug.Log("Interact with " + vehicle.name);

    }

    public void PickupItem(GameObject item)
    {
        Debug.Log(item + " picked up");
        heldItem = item;
        item.transform.SetParent(this.transform);
    }
}
