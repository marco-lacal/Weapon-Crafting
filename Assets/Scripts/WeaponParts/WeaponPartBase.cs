using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need monobehaviour to do transform stuff
public abstract class WeaponPartBase : MonoBehaviour
{
    public StatSheet StatSheet {get => statSheet;}

    [SerializeField] private StatSheet statSheet;
    //List of perks??

    void Awake()
    {
        if(statSheet != null)
        {
            statSheet.SetStatsListForParts();
        }
    }

    public void ClearList()
    {
        statSheet.ClearList();
    }

    //ToBeParent will be a corresponding BlankToBody part
    public void AttachToPart(Transform ToBeParent)
    {
        transform.parent = ToBeParent;

        transform.position = ToBeParent.position;
        transform.rotation = ToBeParent.rotation;
    }
}
