using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ZoomAction
{
    In,
    Out
}

//The Idle state is divided into an ADS Idle and HipFire Idle

//The weaponstatemanager derives from Subject so that it can access camera observers,
//as well as have access to Monobehaviour stuff. We do this so that we dont make the ADS and HF
//states inherit Subject and thus take on all of Monbehaviour.
public class WeaponStateManager : Subject
{
    public Transform EquippedWeapon {get {return equippedWeapon;}}
    public StatSheet Stats {get {return stats;}}
    public WeaponType FiringType {get {return firingType;}}
    public Vector3 HF {get {return hipfire;}}
    public float LastShot {get {return lastShot;}}
    public int BurstSize {get {return burstSize;}}
    public float BurstDelay {get {return burstDelay;}}
    public float ADSSpeed {get {return adsSpeed;}}

    private Transform equippedWeapon;
    private StatSheet stats;

    //Stuff from barrel, body, and scope that is needed
    private ParticleSystem muzzleFlash;
    private WeaponType firingType;
    private Transform reticle;

    private int burstSize;
    private float burstDelay;
    
    private float adsSpeed;

    private float lastShot;

    private Vector3 hipfire;

    private GameObject bulletHolePrefab;

    [SerializeField] private AudioClip gunshotClip;
    [SerializeField] private AudioSource gunshotSource;
    [SerializeField] private GameObject DamageNumbers;

    //Recoil Event
    private RecoilEventManager recoilManager;

    public WeaponBaseState currState;
    public ADS_State ADS = new ADS_State();
    public ADSShoot_State ADSShoot = new ADSShoot_State();
    public HipFire_State HipFire = new HipFire_State();
    public HipFireShoot_State HipFireShoot = new HipFireShoot_State();
    public Reload_State Reload = new Reload_State();

    void Start()
    {
        recoilManager = GetComponent<RecoilEventManager>();
    }

    //not used anywhere
    public void Notify()
    {
        NotifyObservers();
    }

    public void Notify(ZoomAction type, int zoomFactor, float speed)
    {
        NotifyObservers(type, zoomFactor, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(equippedWeapon == null)
        {
            return;
        }

        // TEMPORARY FIX TO PREVENT PLAYER ACTIONS WHILE IN MENUS
        if(Time.timeScale == 0f)
        {
            return;
        }

        currState.UpdateState(this);
    }

    public void SwitchState(WeaponBaseState state)
    {
        currState.ExitState(this);
        currState = state;
        state.EnterState(this);
    }

    public void DropWeapon()
    {
        Debug.Log("Dropping weapon");

        //Notify();

        equippedWeapon.parent = null;
        equippedWeapon.position += Vector3.up + transform.forward;

        equippedWeapon.GetComponent<WeaponPickup>().enabled = true;

        equippedWeapon.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);

        equippedWeapon = null;
    }

    //Helper functions that will be called within the different states

    public Transform RetrieveR()
    {
        return reticle;
    }

    public void RefillMagazine()
    {
        if(stats.InventorySize >= stats.Magazine)
        {
            stats.InventorySize -= stats.Magazine - stats.PracticalMagazine;
            stats.PracticalMagazine = stats.Magazine;
        }
        else
        {
            stats.PracticalMagazine = stats.InventorySize;
            stats.InventorySize = 0;
        }
        
    }

    public void DecreaseMag()
    {
        stats.PracticalMagazine--;
    }

    public void DetermineNextShot()
    {
        //Adjust this formula until desired results
        float nextShot = 1/(((float)Stats.RateOfFire)/60);
        lastShot = Time.time + nextShot;
    }

    public void PlayGunShot()
    {
        float randomVolume = Random.Range(0.5f, 0.6f);

        gunshotSource.PlayOneShot(gunshotClip, randomVolume);
    }

    public void AddRecoil(bool isADS, Vector3 startingEulerAngles)
    {
        recoilManager.OnShoot(stats, firingType, isADS, startingEulerAngles);
    }

    //try changing the prefab gameObject to just being a texture object so having multiple of them isnt too taxing
    public void CreateBulletHole(RaycastHit hit)
    {
        GameObject temp = Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(temp, 5f);
    }

    public void ShowDamageNumbers(RaycastHit hit, float damage)
    {
        Vector3 position = hit.point + Vector3.Normalize(hit.normal);

        GameObject temp = Instantiate(DamageNumbers, position, Quaternion.LookRotation(hit.normal));

        Transform text = temp.transform.GetChild(0);
        text.LookAt(transform);
        text.Rotate(0, 180, 0);
        text.GetComponent<TextMeshPro>().text = damage.ToString();

        Destroy(temp, 1f);
    }

    //Set-up functions below

    public void SetEquippedWeaponAndStats(Transform weapon, StatSheet wS)
    {
        //will replace this with weaponpickups / drops
        if(equippedWeapon != null)
        {
            DropWeapon();
        }

        equippedWeapon = weapon;
        hipfire = equippedWeapon.localPosition;

        GetImportantReferences();

        //can now get the parts list from Statsheet
        stats = wS;
        adsSpeed = 0.015f + ((float)Stats.Handling / 2222);

        //replace this with each individual gunshot noise that will be assigned to some part and pulled from GetImportantReferences()
        if(gunshotSource == null)
        {
            gunshotSource = gameObject.AddComponent<AudioSource>();
            gunshotSource.clip = gunshotClip;
            gunshotSource.volume = 0.3f;
        }
        
        currState = HipFire;
        currState.EnterState(this);
    }

    private void GetImportantReferences()
    {
        muzzleFlash = equippedWeapon.GetChild(0).GetComponent<Barrel>().MuzzleFlash;
        firingType = equippedWeapon.GetChild(1).GetComponent<Body>().FiringType;
        reticle = equippedWeapon.GetChild(4).GetComponent<Scope>().Reticle;
        burstSize = equippedWeapon.GetChild(1).GetComponent<Body>().BurstSize;
        burstDelay = equippedWeapon.GetChild(1).GetComponent<Body>().BurstDelay;
        bulletHolePrefab = equippedWeapon.GetChild(3).GetComponent<Magazine>().BulletHole;
        //audio clip
        //idk something else mayber
    }

    /*THERE ARE TWO OPTIONS TO CHOSE FROM:
        - CREATE STATSHEET FOR THE WEAPONSTATEMANAGER THAT WILL BE SET TO EQUAL TO SUM OF ALL PARTS
        - NO STATSHEET, INSTEAD JUST HAVE 9 VARIABLES FOR EACH STAT IN THE STATE MANAGER
    */
}
