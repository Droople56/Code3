using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Criminal : MonoBehaviour
{
    List<Transform> waypoints;
    NavMeshAgent agent;
    [SerializeField]
    bool reachedPosition = true;

    int currentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypoints = new List<Transform>();

        var vehicles = GameObject.FindGameObjectsWithTag("Vehicle").Where(x => x.gameObject.name.Contains("CivilianVehicle")).Select(x => x.transform).ToList();
        var houses = GameObject.FindGameObjectsWithTag("House").Select(x => x.transform).ToList();

        waypoints.AddRange(houses);
        waypoints.AddRange(vehicles);
    }

    // Update is called once per frame
    void Update()
    {
        // If he made it to the target position, select a new random waypoint to go to
        if (reachedPosition)
        {
            var randomPosition = Random.Range(0, waypoints.Count);
            agent.SetDestination(waypoints[randomPosition].position);
            reachedPosition = false;
            currentWaypointIndex = randomPosition; 
        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if he made it to the current waypoint
        if(other.gameObject.name.Contains("CivilianVehicle") || other.gameObject.name.Contains("House")) 
        {
            if(waypoints[currentWaypointIndex].position == other.gameObject.transform.position)
            {
                reachedPosition = true;
            }
        }
    }
}
