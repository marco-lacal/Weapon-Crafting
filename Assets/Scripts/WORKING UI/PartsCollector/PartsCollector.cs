using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsCollector : MonoBehaviour, EquipObserver
{
    public int RifleCount {get {return numRifles;}}
    public int SMGCount {get {return numSMGs;}}
    public int PistolCount {get {return numPistols;}}
    public int SwordCount {get {return numSwords;}}

    public int RiflePartsCount {get {return numRifleParts;}}
    public int SMGPartsCount {get {return numSMGParts;}}
    public int PistolPartsCount {get {return numPistolParts;}}
    public int SwordPartsCount {get {return numSwordParts;}}


    private int[][] rifleParts;
    private int[][] smgParts;
    private int[][] pistolParts;
    private int[][] swordParts;

    // private List<int[][]> allParts;

    [SerializeField] private int numRifles;
    [SerializeField] private int numSMGs;
    [SerializeField] private int numPistols;
    [SerializeField] private int numSwords;

    [SerializeField] private int numRifleParts;
    [SerializeField] private int numSMGParts;
    [SerializeField] private int numPistolParts;
    [SerializeField] private int numSwordParts;

    // Stores the number of parts collected for each weapon type. 
    // Number of parts for the i-th index weapon is PartsCollection[i]
    // 0 : RifleParts   1 : SMGParts    2 : PistolParts     3 : SwordParts
    private int[] partsCollection = new int[4];

    // Stores the number of complete weapon patterns for each weapon type.
    // Number of complete weapon patterns for the ith weapon is CompleteWeaponsCollection[i]
    // 0 : RifleParts   1 : SMGParts    2 : PistolParts     3 : SwordParts
    private int[] completeWeaponsCollection = new int[4];

    public int GetIndexedParts(int weaponType)
    {
        return partsCollection[weaponType];
    }

    public int GetCompletePatterns(int weaponType)
    {
        return completeWeaponsCollection[weaponType];
    }

    public int GetNumberOfParts(int weaponType)
    {
        int temp = 0;

        switch(weaponType)
        {
            case 0:
                temp = numRifleParts;
                break;
            case 1:
                temp = numSMGParts;
                break;
            case 2:
                temp = numPistolParts;
                break;
            case 3:
                temp = numSwordParts;
                break;
            default:
                Debug.Log("Oh No");
                break;
        }

        return temp;
    }

    public int GetNumberOfEachWeapon(int weaponType)
    {
        int temp = 0;

        switch(weaponType)
        {
            case 0:
                temp = numRifles;
                break;
            case 1:
                temp = numSMGs;
                break;
            case 2:
                temp = numPistols;
                break;
            case 3:
                temp = numSwords;
                break;
            default:
                Debug.Log("Oh No");
                break;
        }

        return temp;
    }

    public int[][] GetPartsArray(int weaponType)
    {
        int[][] partsArray = rifleParts;

        switch(weaponType)
        {
            case 0:
                // already set to rifleParts
                break;
            case 1:
                partsArray = smgParts;
                break;
            case 2:
                partsArray = pistolParts;
                break;
            case 3:
                partsArray = swordParts;
                break;
        }

        return partsArray;
    }

    void Start()
    {
        /*
            Add 1 more row to each 2D array to store a int flag (0/1)
            Will determine if all parts associated with a particular weapon pattern are collected

            I.E. Rifle:          |  0  |  1  |  2  |  ...
            0 BAR                |  1  |  0  |  1  |
            1 BOD                |  1  |  0  |  1  |
            2 GRI                |  1  |  1  |  1  |
            3 MAG                |  1  |  1  |  0  |
            4 SCO                |  1  |  0  |  1  |
            5 STO                |  1  |  1  |  1  |
            6 CompletePattern    |  1  |  0  |  0  |

            Will be used for the weapon crafting screens that will display:
            1) Number of completed frames present
            2) Number of collected parts (PartsCollection)
        */

        rifleParts = new int[numRifleParts + 1][];

        for(int i = 0; i < rifleParts.Length; i++)
        {
            rifleParts[i] = new int[numRifles];
        }

        smgParts = new int[numSMGParts + 1][];

        for(int i = 0; i < smgParts.Length; i++)
        {
            smgParts[i] = new int[numSMGs];
        }

        pistolParts = new int[numPistolParts + 1][];

        for(int i = 0; i < pistolParts.Length; i++)
        {
            pistolParts[i] = new int[numPistols];
        }

        swordParts = new int[numSwordParts + 1][];

        for(int i = 0; i < swordParts.Length; i++)
        {
            swordParts[i] = new int[numSwords];
        }

        // allParts = new List<int[][]>
        // {
        //     rifleParts,
        //     smgParts,
        //     pistolParts,
        //     swordParts
        // };

        Debug.Log(ScreenManager.Instance);
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddEObserver((EquipObserver)this);
    }

    void OnEnable()
    {
        //add this object to the subject's list of subscribers. will be WSM
        if(ScreenManager.Instance != null) ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddEObserver((EquipObserver)this);
    }

    void OnDisable()
    {
        //remove this object from the subject's list of subscribers. will be WSM
        if(ScreenManager.Instance != null && ScreenManager.Instance.WSM != null) ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveEObserver((EquipObserver)this);
    }

    // public int[] GetSpecificRow(int typeID, int partID)
    // {
    //     return allParts[typeID][partID];
    // }

    public void OnNotify_Equip(StatSheet stats){}

    public void OnNotify_Unequip(){}

    public void OnNotify_EquipParts(int[] weaponParts , int weaponType)
    {
        // for(int i = 0; i < weaponParts.Length; i++)
        // {
        //     Debug.Log(weaponParts[i]);
        // }

        // Debug.Log(rifleParts.Length + "  " + rifleParts[0].Length + "  " + weaponParts.Length);

        int[][] partsArray = GetPartsArray(weaponType);

        for(int i = 0; i < weaponParts.Length; i++)
        {
            // - 1 because weapon parts are 1-based indexing not 0-based
            if(partsArray[i][weaponParts[i] - 1] == 0)
            {
                partsArray[i][weaponParts[i] - 1] = 1;
                partsCollection[weaponType]++;  // Update the count of individual parts found

                // Check if the weapon pattern this part belongs to is complete (last row contains these values)
                // If not ( == 0), then loop through that weapon's parts. If any part is 0, dont continue
                // If all parts are == 1, then change 3rd dimension to 1 to indicate complete weapon
                if(partsArray[6][weaponParts[i] - 1] == 0)
                {
                    int counter = 0;

                    for(int j = 0; j < partsArray.Length - 1; j++)
                    {
                        if(partsArray[j][weaponParts[i] - 1] == 0)
                        {
                            // The jth part of weapon i-1 hasn't been collected yet
                            break;
                        }

                        counter++;
                    }

                    if(counter == partsArray.Length - 1)
                    {
                        Debug.Log("COMPLETED WEAPON " + weaponParts[i]);
                        partsArray[6][weaponParts[i] - 1] = 1;
                        completeWeaponsCollection[weaponType]++;    // Update the count of complete weapon patterns
                    }
                }
                // Else: do nothing
            }
        }

        // Debug.Log("PRINT EVERYTHING");

        // for(int i = 0; i < partsArray.Length; i++)
        // {
        //     string print = i.ToString() + "== ";
        //     for(int j = 0; j < partsArray[0].Length; j++)
        //     {
        //         print += j + ": " + partsArray[i][j] + ", ";
        //     }

        //     Debug.Log(print);
        // }
    }
}
