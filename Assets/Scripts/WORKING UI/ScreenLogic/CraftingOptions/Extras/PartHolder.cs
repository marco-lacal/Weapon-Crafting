using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    Barrel = 0,
    Body = 1,
    Grip = 2,
    Magazine = 3,
    Scope = 4,
    Stock = 5
}

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
    private int initializationValue;    // number of weapons used to check if the dropdowns are being initialized

    /*
        Each time a part is selected by the dropdown menu script, the part type and a number for the weapon it belongs to is sent here.
        Then this script constructs a string based on the part type and weapon number to use to search through the Resources folder and retrieve the reference.
        Because of this, a valid sequence of part selections is: barrel_1, barrel_3, barrel_1, barrel_3, barrel_1, barrel_3, barrel_1, barrel_3, ...; causing the script
        to build the same two strings, search for the same two references based on the strings, then retrieve the reference.
        
        If we instead store all weapon parts the user selected in this current iteration into a Dictionary/Hashtable, 
        anytime a repeat part is wanted we can get it faster from the Dictionary.
        
        To create unique keys to store the approriate reference, the script will create an integers that are a combination of the part type and the weapon number
        Note: both values will be 1-based indexing, so Barrel = 1, Body = 2, etc
        Example: Barrel_4 -> 14; Scope_2 -> 42; Body_12 (assuming there are that many) -> 212
    */

    private Dictionary<int, GameObject> repeatParts;

    private string startOfPath;

    public delegate void SendToStats(StatSheet newPart, int partType);

    void Awake()
    {
        SelectionsManager.InformOtherComponents += GatherParts;

        BaseCrafting tempB = transform.parent.GetComponent<BaseCrafting>();

        weaponType = tempB.WeaponTypeID;
        
        switch(weaponType)
        {
            case 0:
                startOfPath = "WeaponParts/Rifle/Weapon";
                break;
            case 1:
                startOfPath = "WeaponParts/Pistol/Weapon";
                break;
            case 2:
                startOfPath = "WeaponParts/SMG/Weapon";
                break;
            case 3:
                startOfPath = "WeaponParts/Sword/Weapon";
                break;
        }

        int temp = tempB.NumberOfWeaponParts;
        
        gameobjectPartsArray = new GameObject[temp];

        previousParts = new int[temp];
        Array.Fill(previousParts, -1);

        repeatParts = new Dictionary<int, GameObject>();
    }

    void OnDisable()
    {
        SelectionsManager.InformOtherComponents -= GatherParts;
    }

    public void GatherParts(int partType /*index essentially*/, int weaponNumber)
    {
        if(weaponNumber == -1)
        {
            sDisplay.SubtractPart(partType);
            vDisplay.Make3DModel(partType, null);

            return;
        }

        int hash = Int32.Parse((partType + 1).ToString() + (weaponNumber + 1).ToString());

        if(repeatParts.TryGetValue(hash, out GameObject foundRepeat))
        {
            Debug.Log("DUPLICATE");
            gameobjectPartsArray[partType] = foundRepeat;
        }
        else
        {
            string temp = startOfPath + (weaponNumber+1).ToString() + "/" + ((PartType)partType).ToString() + "_" + (weaponNumber+1);

            gameobjectPartsArray[partType] = (GameObject)Resources.Load(temp);

            repeatParts[hash] = gameobjectPartsArray[partType];
        }
        
        StatSheet newPart = gameobjectPartsArray[partType].GetComponent<WeaponPartBase>().StatSheet;
        
        if(partType == 1)
        {
            newPart.Name = gameobjectPartsArray[partType].GetComponent<Body>().Name;
        }

        sDisplay.AddPart(partType, newPart);
        vDisplay.Make3DModel(partType, gameobjectPartsArray[partType]);
    }

    // public void GatherParts(int[] parts, bool isCompleteWeapon)
    // {
    //     int index;
    //     string temp = "";

    //     for(index = 0; index < parts.Length; index++)
    //     {
    //         // if a part hasn't been selected OR the part hasn't been changed
    //         if(parts[index] == -1 || previousParts[index] == parts[index])
    //         {
    //             continue;
    //         }
    //         else    // we have found the part that is updated. Add/Update that part then call the other events and end this function
    //         {

    //             if(parts[index] == initializationValue)
    //             {
    //                 return;
    //             }

    //             // logic to use the addressables to search based on labels to populate GOpartsarray
    //             // gameobjectPartsArray[index] = Gameobject with the 
    //             temp += startOfPath + (parts[index]+1).ToString() + "/" + ((PartType)index).ToString() + "_" + (parts[index]+1);

    //             Debug.Log(temp);

    //             gameobjectPartsArray[index] = (GameObject)Resources.Load(temp);

    //             Debug.Log(gameobjectPartsArray[index] + "   " + sDisplay);

    //             sDisplay.AddPart(gameobjectPartsArray[index].GetComponent<WeaponPartBase>().StatSheet);
    //             vDisplay.Make3DModel(gameobjectPartsArray[index]);

    //             previousParts[index] = parts[index];

    //             return;
    //         }
    //     }


    // }


}
