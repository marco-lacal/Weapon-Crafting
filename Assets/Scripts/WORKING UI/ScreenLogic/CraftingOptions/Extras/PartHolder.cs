using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will be a subscriber to the SelectionsManager event
// It will take in the parts array and store an array of each parts gameobject
// Have this holder class because StatsDisplay wants a single StatsSheet to add/remove
// and because ViewDisplay wants a single gameobject
public class PartHolder : MonoBehaviour
{
    [SerializeField] private StatsDisplay sDisplay;
    [SerializeField] private Viewport vDisplay;

    // cache the previous parts array so I don't have to check unchanged values
    private int[] previousParts;

    private GameObject[] gameobjectPartsArray;
    private int weaponType;

    public delegate void SendToStats(StatSheet newPart, int partType);


    void Awake()
    {
        SelectionsManager.InformOtherComponents += GatherParts;

        weaponType = transform.parent.GetComponent<BaseCrafting>().WeaponTypeID;

        int temp = ScreenManager.Instance.parts.GetNumberOfParts(weaponType);
        
        gameobjectPartsArray = new GameObject[temp];

        previousParts = new int[temp];
        Array.Fill(previousParts, -1);
    }

    public void GatherParts(int[] parts, bool isCompleteWeapon)
    {
        int index;

        string temp = "";
        string temp2 = "";

        for(int i = 0; i < parts.Length; i++)
        {
            temp += parts[i] + " ";
            temp2 += previousParts[i] + " ";
        }

        Debug.Log(temp + "   " + temp2);

        for(index = 0; index < parts.Length; index++)
        {
            // if a part hasn't been selected OR the part hasn't been changed
            if(parts[index] == -1 || previousParts[index] == parts[index])
            {
                continue;
            }
            else    // we have found the part that is updated. Add/Update that part then call the other events and end this function
            {
                Debug.Log("NEW PART: " + parts[index] + ", NEW PART TYPE: " + index);

                // logic to use the addressables to search based on labels to populate GOpartsarray
                // gameobjectPartsArray[index] = Gameobject with the 

                // sDisplay.AddPart(gameobjectPartsArray[index].statsheet)
                // vDisplay.Make3DModel(gameobjectPartsArray[index])

                previousParts[index] = parts[index];

                return;
            }
        }
    }


}
