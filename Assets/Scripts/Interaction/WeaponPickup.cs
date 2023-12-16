using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Interactable
{
    // private Vector3 inherentPosition;

    // private Transform weapon;

    // void Awake()
    // {
    //     Debug.Log("Hello from the WeaponPickup Script");

    //     weapon = transform.GetChild(0);

    //     weapon.GetComponent<Banshee45>().Creation(weapon);

    //     weapon.GetComponent<StatsCreation>().CalculateStats();

    //     //this is the correct location vector3 we will use when putting the weapon in the players hands.
    //     //however, for right now we dont want that position so we'll store it here for later.
    //     //then, when we put the weapon in the player hand, we'll use this position.
    //     inherentPosition = weapon.GetChild(1).localPosition;

    //     weapon.transform.position -= inherentPosition;

    //     //make recursive function to set everything to not weapon layer
    // }

    // public override void DisplayPrompt(Transform canvas)
    // {
    //     base.DisplayPrompt(canvas);

    //     //do shit to display the stats ui
    // }

    // public override void Interact(Transform weaponHolder)
    // {
    //     base.Interact(weaponHolder);

    //     //do transfer to player shenanigans: either through
    //     weapon.parent = weaponHolder;
    //     weapon.rotation = weaponHolder.rotation;
    //     weapon.localPosition = Vector3.zero;

    //     weaponHolder.GetComponent<WeaponStateManager>().SetEquippedWeapon(weapon);

    //     Destroy(transform.gameObject);
    // }
    
    //the local position to place this gun at when it's equipped to the player
    private Vector3 inherentPosition;

    private StatSheet stats;

    // Need to store them for PartsCollector to populate crafting menus
    private int[] weaponParts;

    //an additional prompt prefab for the weapon stats display
    [SerializeField] private GameObject prefabSheet;
    private GameObject instSheet;

    void Awake()
    {
        //create gun
        inherentPosition = transform.GetComponent<Banshee45>().Creation(transform, null);

        weaponParts = transform.GetComponent<Banshee45>().WeaponParts;

        Destroy(transform.GetComponent<Banshee45>());

        //create stats
        stats = transform.GetComponent<StatsCreation>().CalculateStats();

        Destroy(transform.GetComponent<StatsCreation>());

        //USE THIS FOR TESTING LATER
        //PrintStats();

        //transform.position -= inherentPosition;

        //make recursive function to set everything to not weapon layer
        //VERY IMPORTANT TO DO
    }

    //when this script is enabled, it means its either been spawned for the first time or been dropped
    void OnEnable()
    {
        hasBeenInteractedWith = false;

        //enable BoxCollider so the raycast from player camera can collide with this object again
        transform.GetComponent<BoxCollider>().enabled = true;

        //set the layers to Interactable from Weapon
        RecursivelyChangeLayers(transform, 8);

        //for physics shenanagins
        transform.gameObject.AddComponent<Rigidbody>();
    }

    //whenever the gun is picked up
    void OnDisable()
    {
        // this statement equates to true whenever the game is ended and all objects are disabled
        if(transform.parent == null)
        {
            return;
        }
        
        transform.GetComponent<BoxCollider>().enabled = false;
        
        //set gun layers to Weapon so weapon camera can see it properly
        RecursivelyChangeLayers(transform, 6);

        Destroy(transform.GetComponent<Rigidbody>());
    }

    private void PrintStats()
    {
        Debug.Log("Rate of fire: " + stats.RateOfFire);
        Debug.Log("Damage: " + stats.Damage);
        Debug.Log("Range: " + stats.Range);
        Debug.Log("Stability: " + stats.Stability);
        Debug.Log("Recoil Control: " + stats.RecoilControl);
        Debug.Log("Handling: " + stats.Handling);
        Debug.Log("Reload Speed: " + stats.ReloadSpeed);
        Debug.Log("Magazine: " + stats.Magazine);
        Debug.Log("Zoom Factor: " + stats.ZoomFactor);
    }

    public override void DisplayPrompt(Transform canvas)
    {
        base.DisplayPrompt(canvas);

        //do shit to display the stats ui
        if(instSheet == null)
        {
            instSheet = Instantiate(prefabSheet, canvas);
            instSheet.GetComponent<WSS>().CreateStatSheet(stats);
            //instPrompt.transform.parent = canvas;
        }
    }

    public override void DestroyPrompt()
    {
        base.DestroyPrompt();

        if(instSheet != null)
        {
            Destroy(instSheet);
        }
    }

    public override void Interact(Transform weaponHolder)
    {
        base.Interact(weaponHolder);

        transform.GetComponent<BoxCollider>().enabled = false;

        //do transfer to player shenanigans: either through
        transform.parent = weaponHolder;
        transform.rotation = weaponHolder.rotation;
        transform.localPosition = inherentPosition;

        weaponHolder.GetComponent<WeaponStateManager>().SetEquippedWeaponAndStats(transform, stats, weaponParts);

        //Destroy(transform.gameObject);

        this.enabled = false;
    }

    //layer 6 = Weapons, layer 8 = Interactable
    void RecursivelyChangeLayers(Transform obj, int newLayer)
    {
        obj.gameObject.layer = newLayer;

        foreach(Transform child in obj)
        {
            RecursivelyChangeLayers(child, newLayer);
        }
    }
}
