using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will be accessed direfctly by PartHolders
public class Viewport : MonoBehaviour
{
    private int weaponType;

    void Awake()
    {
        weaponType = transform.parent.GetComponent<BaseCrafting>().WeaponTypeID;
    }

    public void Make3DModel(int partType, GameObject newPart)
    {
        // ...
    }
}
