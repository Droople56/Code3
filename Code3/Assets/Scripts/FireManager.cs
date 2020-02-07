using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public float instantiationTimer = 1f;
    private float radius = 1f;

    public GameObject firePrefab;
    private List<GameObject> fires;

    public LayerMask fireLayer;

    // Start is called before the first frame update
    void Start()
    {
        fires = new List<GameObject>();
        StartFire();
    }

    // Update is called once per frame
    void Update()
    {
        CreateFire();
    }

    void StartFire()
    {
        var newFire = Instantiate(firePrefab, new Vector3(0,0,0), Quaternion.identity);
        fires.Add(newFire);
    }

    void CreateFire()
    {
        instantiationTimer -= Time.deltaTime;
        if (instantiationTimer <= 0)
        {
            var position = CalculateRandomPosition();

            do
            {
                position = CalculateRandomPosition();
            }
            while (Physics.OverlapSphere(position, .25f, fireLayer).Length > 0);

            var fireObj = Instantiate(firePrefab, position, Quaternion.identity);
            instantiationTimer = 2f;
            fires.Add(fireObj);
        }
    }

    public Vector3 CalculateRandomPosition()
    {
        var lastFireObj = fires.Last();
        var firePosition = lastFireObj.transform.position;
        var fireRadius = lastFireObj.GetComponentInChildren<ParticleSystem>().shape.radius;
        var randomDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)).normalized;
        var randomPosition = firePosition + (randomDirection * fireRadius) + Random.insideUnitSphere * radius;
        randomPosition.y = 0;
        return randomPosition;
    }
}
