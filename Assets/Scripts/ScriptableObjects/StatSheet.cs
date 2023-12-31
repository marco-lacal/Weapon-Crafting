using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(menuName = "StatSheet")]
public class StatSheet : ScriptableObject
{
    //getters for every stat
    public int RateOfFire {get {return rateOfFire; }}
    public int Damage {get {return damage; }}
    public int Range {get {return range; }}
    public int Stability {get {return stability; }}
    public int RecoilControl {get {return recoilControl; }}
    public int Handling {get {return handling; }}
    public int ReloadSpeed {get {return reloadSpeed; }}
    public int Magazine {get {return magazine; }}
    public int ZoomFactor {get {return zoomFactor; }}
    public int InventorySize {get {return inventorySize; } set {inventorySize = value;}}

    //this value stores the amount of bullets remaining
    public int PracticalMagazine {get {return practicalMagazine;} set {practicalMagazine = value;}}

    public List<int> StatsList {get {return statsList; }}

    public string Name {get {return weaponName;} set {weaponName = value;}}

    public int[] PartsList {get {return partsList; } set {partsList = value;}}

    [SerializeField] private int rateOfFire;
    [SerializeField] private int damage;
    [SerializeField] private int range;
    [SerializeField] private int stability;
    [SerializeField] private int recoilControl;
    [SerializeField] private int handling;
    [SerializeField] private int reloadSpeed;
    [SerializeField] private int magazine;
    [SerializeField] private int zoomFactor;
    [SerializeField] private int inventorySize;

    //indices 0-5 refer to each part alphabetically (0 = Barrel; 1 = Body; etc). values range between 1 and the number of weapons of some type
    //used in the reforge process that will need to access what parts are used
    // Not sure about this anymore
    private int[] partsList;
    
    private int practicalMagazine;

    private string weaponName;

    //List of ints to store this StatSheet's stats that will be accessed in StatsCreation
    private List<int> statsList;

    void Awake()
    {
        weaponName = "";

        partsList = new int[6];
    }

    //on awake, create a List containing the stats
    public void SetStatsListForParts()
    {
        statsList = new List<int>
        {
            rateOfFire,
            damage,
            range,
            stability,
            recoilControl,
            handling,
            reloadSpeed,
            magazine,
            zoomFactor,
            inventorySize
        };
    }

    //set the StatSheet stats equal to the passed in List values
    public void SetStatSheetToStatsList(List<int> stats)
    {
        rateOfFire = stats[0];
        damage = stats[1];
        range = stats[2];
        stability = stats[3];
        recoilControl = stats[4];
        handling = stats[5];
        reloadSpeed = stats[6];
        magazine = stats[7];
        practicalMagazine = magazine;
        zoomFactor = stats[8];
        inventorySize = stats[9];

        ClearList();
    }

    public void ClearList()
    {
        if(statsList != null)
        {
            statsList.Clear();
        }
    }
}
