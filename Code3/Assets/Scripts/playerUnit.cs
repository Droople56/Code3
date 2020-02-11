using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        unitPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixUnitPosition()
    {
        unitPos = transform.position;
    }

    public void MoveUnit(Vector3 destination)
    {
        transform.LookAt(destination);
        
        transform.position = Vector3.Lerp(unitPos, destination, 5.0f);

    }


}
