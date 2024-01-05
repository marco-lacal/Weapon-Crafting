using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Interactable
{    
    //the local position to place this gun at when it's equipped to the player
    private Vector3 inherentPosition;

    private BoxCollider hitbox;
    private Rigidbody physics;

    private StatSheet stats;

    // Need to store them for PartsCollector to populate crafting menus
    private int[] weaponParts;

    private int weaponType;

    //an additional prompt prefab for the weapon stats display
    [SerializeField] private GameObject prefabSheet;
    private GameObject instSheet;

    void Awake()
    {
        // ALL OBJECTS THAT WILL INSTANTIATE WEAPONPICKUP WILL BE MADE PARENT FIRST SO THIS SHOULD ALWAYS RETURN THE CORRECT COMPONENT
        transform.parent.GetComponent<CreateWeaponInterface>().CreateWeaponEvent += MakeWeaponObject;

        hitbox = transform.GetComponent<BoxCollider>();
    }

    public void MakeWeaponObject(int type, int[] parts)
    {
        // REMOVE SELF FROM EVENT
        transform.parent.GetComponent<CreateWeaponInterface>().CreateWeaponEvent -= MakeWeaponObject;

        // OBJECT SETUP

        // it is no longer a child of its individual weaponcrate
        transform.parent = null;

        //enable BoxCollider so the raycast from player camera can collide with this object again
        hitbox.enabled = true;

        //for physics shenanagins
        physics = transform.gameObject.AddComponent<Rigidbody>();

        // WEAPON CREATION CALLS

        // Will always be between 0 and number of weapon types - 1. The weapon crates will generate from this range and the crafting screens will send their value
        weaponType = type;

        // Save the parts into our weaponParts variable to later give the part numbers to WSM for PartsCollector
        weaponParts = parts;

        //create gun - if parts was null, then Banshee45 will generate parts
        inherentPosition = transform.GetComponent<Banshee45>().Creation(transform, weaponParts, weaponType);
        weaponParts = transform.GetComponent<Banshee45>().WeaponParts;
        Destroy(transform.GetComponent<Banshee45>());

        //create stats
        stats = transform.GetComponent<StatsCreation>().CalculateStats();
        Destroy(transform.GetComponent<StatsCreation>());
    }

    //when this script is enabled, it means its either been spawned for the first time or been dropped
    void OnEnable()
    {
        hasBeenInteractedWith = false;

        if(inherentPosition != Vector3.zero)
        {
            //enable BoxCollider so the raycast from player camera can collide with this object again
            Debug.Log("WAAAA");
            hitbox.enabled = true; 
            physics = transform.gameObject.AddComponent<Rigidbody>();
        }

        //set the layers to Interactable from Weapon
        RecursivelyChangeLayers(transform, 8);
    }

    //whenever the gun is picked up
    void OnDisable()
    {
        // this statement equates to true whenever the game is ended and all objects are disabled
        if(transform.parent == null)
        {
            return;
        }
        
        hitbox.enabled = false;
        
        //set gun layers to Weapon so weapon camera can see it properly
        RecursivelyChangeLayers(transform, 6);

        // only check null incase game quits before rigidbody is created (between open chest and MakeWeapon)
        if(physics != null) Destroy(physics);
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

        hitbox.enabled = false;

        //do transfer to player shenanigans: either through
        transform.parent = weaponHolder;
        transform.rotation = weaponHolder.rotation;
        transform.localPosition = inherentPosition;

        weaponHolder.GetComponent<WeaponStateManager>().SetEquippedWeaponAndStats(transform, stats, weaponParts, weaponType);

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
