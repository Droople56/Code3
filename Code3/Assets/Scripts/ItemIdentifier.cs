using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdentifier : MonoBehaviour
{
    /// <summary>
    /// MEDICAL IDs:
    /// Medkit = 1001
    /// Defibulator = 1002
    /// 
    /// POLICE IDs:
    /// Baton = 2001
    /// 
    /// FIRE IDs:
    /// Fire Extinguisher = 3001
    /// Hose = 3002
    /// Jaws = 3003
    /// 
    /// </summary>


    public int ID; //Identifier for the type of item
    // Start is called before the first frame update
    void Start()
    {
        ID = -1;
    }

    public void SetItemID(int num)
    {
        ID = num;
    }


    public int GetID()
    {
        if (ID == -1)
        {
            throw new System.Exception("Item ID not defined.");
        }

        return ID;
    }

}
