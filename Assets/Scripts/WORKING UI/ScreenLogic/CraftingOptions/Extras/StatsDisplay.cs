using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Will be accessed directly by PartHolder
public class StatsDisplay : MonoBehaviour
{
    private int weaponType;

    void Awake()
    {
        weaponType = transform.parent.GetComponent<BaseCrafting>().WeaponTypeID;
    }

    public void AddPart(StatSheet newPart)
    {
        // ...
    }
}
