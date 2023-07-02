using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCreation : MonoBehaviour
{
    //blank StatSheet that I will create a copy of
    public StatSheet toClone;
    //the actual gun stats of the final product
    private StatSheet weaponStats;

    public StatSheet CalculateStats()
    {
        // foreach(Transform child in equippedWeapon)
        // {
        //     Debug.Log("Child name: " + child.name + ", and the WeaponPartBase: " + child.GetComponent<WeaponPartBase>());
        //     parts.Add(child.GetComponent<WeaponPartBase>().StatSheet);
        // }

        weaponStats = Instantiate(toClone);

        //List of StatSheet to that will store the StatSheets of all gun parts
        List<StatSheet> parts = new List<StatSheet>();

        //transform.childCount will always be 6
        for(int i = 0; i < transform.childCount; i++)
        {
            //get and add the StatSheet from each weaponpartbase to the list
            parts.Add(transform.GetChild(i).GetComponent<WeaponPartBase>().StatSheet);

            // when index = 1, that is the Body we are looking at, so get the guns's name
            if(i == 1)
            {
                weaponStats.Name = transform.GetChild(i).GetComponent<Body>().Name;
            }
        }

        // Just in case :)
        if(parts.Count != 6)
        {
            Debug.Log("Massive Fucking error");
            return null;
        }

        //now create int List that will store combined totals of each stat
        List<int> statsList = new List<int>();

        //10 stats so i: 0->9
        for(int i = 0; i < 10; i++)
        {
            //rate of fire and inventory size, don't want them to be clamped between 0 and 100 like the other stats
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
                //add the ith stat of every part together and clamp it between 0 and 100
                statsList.Add(Mathf.Clamp(parts[0].StatsList[i] + 
                                    parts[1].StatsList[i] + 
                                    parts[2].StatsList[i] +
                                    parts[3].StatsList[i] +
                                    parts[4].StatsList[i] +
                                    parts[5].StatsList[i], 0, 100));
            }
        }

        //empty the List's stored in each StatSheet
        for(int i = 0; i < parts.Count; i++)
        {
            parts[i].ClearList();
        }

        //now empty the List of StatSheets
        parts.Clear();

        //send the int List into our desired StatSheet object to set the values
        weaponStats.SetStatSheetToStatsList(statsList);

        return weaponStats;
    }
}
