using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField]
    private float instantiationTimer = 1f;
    private float radius = 2f;
    private int retryCount = 0;
    [SerializeField]
    private int maxRetries = 20;

    [SerializeField]
    private GameObject firePrefab;
    private List<GameObject> fires;

    [SerializeField]
    private LayerMask fireLayer;

    private bool spreadFire = false;

    // Start is called before the first frame update
    void Start()
    {
        fires = new List<GameObject>();
        StartFire();
    }

    // Update is called once per frame
    void Update()
    {
        if (spreadFire)
        {
            CreateFire();
        }
    }

    /// <summary>
    /// Starts a fire at the location of this game object.
    /// </summary>
    void StartFire()
    {
        var newFire = Instantiate(firePrefab, this.transform.position, Quaternion.identity);
        fires.Add(newFire);
        spreadFire = true;
    }

    /// <summary>
    /// Instantiates another instance of the fire prefab at a random location
    /// </summary>
    void CreateFire()
    {
        instantiationTimer -= Time.deltaTime;
        if (instantiationTimer <= 0)
        {
            var position = CalculateRandomPosition();
            var fireObj = Instantiate(firePrefab, position, Quaternion.identity);
            instantiationTimer = 2f;
            fires.Add(fireObj);
        }
    }

    /// <summary>
    /// Calculates a random position for a new fire prefab. 
    /// Looks for a random position around the last spawned fire prefab.
    /// </summary>
    /// <returns>A random position.</returns>
    public Vector3 CalculateRandomPosition()
    {
        var lastFireObj = fires.Last();
        var firePosition = lastFireObj.transform.position;
        var fireRadius = lastFireObj.GetComponentInChildren<ParticleSystem>().shape.radius;
        var randomDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)).normalized;
        var randomPosition = firePosition + (randomDirection * fireRadius) + Random.insideUnitSphere * radius;
        randomPosition.y = 0;

        if(Physics.OverlapSphere(randomPosition, .25f, fireLayer).Length > 0)
        {
            if(retryCount > maxRetries)
            {
                spreadFire = false;
                return new Vector3(-10000, -10000, -10000);
            }

            retryCount++;
            Debug.Log(retryCount);
            return CalculateRandomPosition();
        }
        else
        {
            retryCount = 0;
        }

        return randomPosition;
    }
}
