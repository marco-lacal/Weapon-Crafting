using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsCollector : MonoBehaviour
{
    public List<int[][]> AllParts {get{return allParts;}}

    private int[][] rifleParts;
    private int[][] smgParts;
    private int[][] pistolParts;
    private int[][] swordParts;

    private List<int[][]> allParts;

    [SerializeField] private int numRifles;
    [SerializeField] private int numSMGs;
    [SerializeField] private int numPistols;
    [SerializeField] private int numSwords;

    [SerializeField] private int numRifleParts;
    [SerializeField] private int numSMGParts;
    [SerializeField] private int numPistolParts;
    [SerializeField] private int numSwordParts;

    void Awake()
    {
        rifleParts = new int[numRifleParts][];

        for(int i = 0; i < numRifleParts; i++)
        {
            rifleParts[i] = new int[numRifles];
        }

        smgParts = new int[numSMGParts][];

        for(int i = 0; i < numSMGParts; i++)
        {
            smgParts[i] = new int[numSMGs];
        }

        pistolParts = new int[numPistolParts][];

        for(int i = 0; i < numPistolParts; i++)
        {
            pistolParts[i] = new int[numPistols];
        }

        swordParts = new int[numSwordParts][];

        for(int i = 0; i < numSwordParts; i++)
        {
            swordParts[i] = new int[numSwords];
        }

        allParts = new List<int[][]>
        {
            rifleParts,
            smgParts,
            pistolParts,
            swordParts
        };
    }

    void OnEnable()
    {
        //add this object to the subject's list of subscribers. will be WSM
    }

    void OnDisable()
    {
        //remove this object from the subject's list of subscribers. will be WSM
    }

    private void PickupNewWeapon()
    {
        
    }

    public int[] GetSpecificRow(int typeID, int partID)
    {
        return allParts[typeID][partID];
    }
}
