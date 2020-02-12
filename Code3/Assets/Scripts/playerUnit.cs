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
    //int identifiers for class type 1 = Firefighter / 2 = Paramedic / 3 = Police Officer
    int unitClass;

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


}
