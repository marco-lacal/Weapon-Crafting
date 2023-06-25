using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCreation : MonoBehaviour
{
    public StatSheet toClone;
    private StatSheet weaponStats;

    public StatSheet CalculateStats()
    {
        // foreach(Transform child in equippedWeapon)
        // {
        //     Debug.Log("Child name: " + child.name + ", and the WeaponPartBase: " + child.GetComponent<WeaponPartBase>());
        //     parts.Add(child.GetComponent<WeaponPartBase>().StatSheet);
        // }

        weaponStats = Instantiate(toClone);

        List<StatSheet> parts = new List<StatSheet>();

        for(int i = 0; i < transform.childCount; i++)
        {
            parts.Add(transform.GetChild(i).GetComponent<WeaponPartBase>().StatSheet);

            if(i == 1)
            {
                weaponStats.Name = transform.GetChild(i).GetComponent<Body>().Name;
            }
        }

        if(parts.Count != 6)
        {
            Debug.Log("Massive Fucking error");
            return null;
        }

        List<int> statsList = new List<int>();

        for(int i = 0; i < 10; i++)
        {

            if(i == 0 || i == 9)
            {
                statsList.Add(parts[0].StatsList[i] + 
                                    parts[1].StatsList[i] + 
                                    parts[2].StatsList[i] +
                                    parts[3].StatsList[i] +
                                    parts[4].StatsList[i] +
                                    parts[5].StatsList[i]);
            }
            else
            {
                statsList.Add(Mathf.Clamp(parts[0].StatsList[i] + 
                                    parts[1].StatsList[i] + 
                                    parts[2].StatsList[i] +
                                    parts[3].StatsList[i] +
                                    parts[4].StatsList[i] +
                                    parts[5].StatsList[i], 0, 100));
            }
        }

        for(int i = 0; i < parts.Count; i++)
        {
            parts[i].ClearList();
        }

        parts.Clear();

        weaponStats.SetStatSheetToStatsList(statsList);

        //Destroy(this);

        return weaponStats;
    }
}
