using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedVehicle : MonoBehaviour
{
    public GameObject injuredCivilian;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RescueCivilian()
    {
        Instantiate(injuredCivilian, new Vector3(transform.position.x + 3.0f, transform.position.y, transform.position.z), Quaternion.Euler(0,0,90));
    }
}
