using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsCollector : MonoBehaviour, EquipObserver
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

    void Start()
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
        if(ScreenManager.Instance != null) ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveEObserver((EquipObserver)this);
    }

    private void PickupNewWeapon()
    {
        
    }

    public int[] GetSpecificRow(int typeID, int partID)
    {
        return allParts[typeID][partID];
    }

    public void OnNotify_Equip(StatSheet stats){}

    public void OnNotify_Unequip(){}

    public void OnNotify_EquipParts(int[] weaponParts /*, int weaponType*/)
    {
        Debug.Log("HELLO");

        // for(int i = 0; i < weaponParts.Length; i++)
        // {
        //     // for now gonna hard code to rifleParts but next will add a int to determine the correct list of parts to add to
        //     rifleParts[i][weaponParts[i]] = 1;
        // }

        for(int i = 0; i < weaponParts.Length; i++)
        {
            Debug.Log(weaponParts[i]);
        }

        Debug.Log(rifleParts.Length + "  " + rifleParts[0].Length + "  " + weaponParts.Length);

        for(int i = 0; i < weaponParts.Length; i++)
        {
            // - 1 because weapon parts are 1-based indexing not 0-based
            rifleParts[i][weaponParts[i] - 1] = 1;
        }
    }
}
