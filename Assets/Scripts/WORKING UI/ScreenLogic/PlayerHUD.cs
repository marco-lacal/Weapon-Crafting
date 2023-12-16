using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour, EquipObserver, ShootObserver, ReloadObserver, ZoomObserver
{
    [Header("Next Screen")]
    [SerializeField] private GameObject nextScreen;

    [Header("Reticle")]
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject weaponReticle;   //obtained from the WSM that ScreenManager has

    private GameObject instWeaponReticle;

    [Header("Array of Weapon Icons")]
    [SerializeField] private GameObject[] weaponIcons;

    [Header("Ammo Text")]
    [SerializeField] private TextMeshProUGUI ammo;

    private int currMag;
    private int currInventory;

    void OnEnable()
    {
        // Don't think I will need this but keep it until im sure
        // if(ScreenManager.Instance.EquippedWeapon != null)
        // {
        //     SetWeaponReticle();
        //     ActivateWeaponIcons();
        // }

        // Potential implementation that would allow me to not manually add each addobserver and removeobserver. However, requires changes to WSMSubject so do this later

        // var temp = GetType().GetInterfaces();
        // foreach(var ass in temp)
        // {
        //     if (ass.IsAssignableFrom(typeof(WSMObserver)))
        //     {
        //         Debug.Log(ass);
        //     }
        //     else
        //     {
        //         Debug.Log("This is not derived from WSMObserver: " + ass);
        //     }
        // }

        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddEObserver((EquipObserver)this);
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddSObserver((ShootObserver)this);
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddRObserver((ReloadObserver)this);
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddZObserver((ZoomObserver)this);

        if(ScreenManager.Instance.WSM.GetComponent<WeaponStateManager>().EquippedWeapon != null)
        {
            SetWeaponReticle();
            ActivateWeaponIcons();

            SetAmmoCounts(ScreenManager.Instance.WSM.GetComponent<WeaponStateManager>().Stats);
        }
    }

    void OnDisable()
    {
        if(this != null && ScreenManager.Instance.WSM != null)
        {
            ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveEObserver((EquipObserver)this);
            ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveSObserver((ShootObserver)this);
            ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveRObserver((ReloadObserver)this);
            ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveZObserver((ZoomObserver)this);
        }
        else
        {
            Debug.Log(this + " , " + ScreenManager.Instance.WSM);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     //equipped weapon
    //     if(Input.GetKeyDown(KeyCode.E))
    //     {
    //         SetWeaponReticle();
    //         ActivateWeaponIcons();
    //     }
    //     else if(Input.GetKeyDown(KeyCode.G))
    //     {
    //         ResetReticle();
    //         DeactivateWeaponIcons();
    //     }
    //     else if(Input.GetKeyDown(KeyCode.Q))
    //     {
    //         ScreenManager.Instance.Push(nextScreen);
    //     }
    // }

    private void SetWeaponReticle()
    {
        reticle.GetComponent<Image>().enabled = false;
        instWeaponReticle = Instantiate(weaponReticle, reticle.transform);
    }

    private void ActivateWeaponIcons()
    {
        foreach(GameObject go in weaponIcons)
        {
            go.SetActive(true);
        }
    }

    private void ResetReticle()
    {
        reticle.GetComponent<Image>().enabled = true;
        Destroy(instWeaponReticle);
    }

    private void DeactivateWeaponIcons()
    {
        foreach(GameObject go in weaponIcons)
        {
            go.SetActive(false);
        }
    }

    private void SetAmmoCounts(StatSheet stats)
    {
        currMag = stats.PracticalMagazine;
        currInventory = stats.InventorySize;

        ammo.text = currMag + " / " + stats.InventorySize;
    }

    public void OnNotify_Equip(StatSheet stats)
    {
        SetWeaponReticle();
        ActivateWeaponIcons();

        SetAmmoCounts(stats);
    }

    // Have the PlayerHUD pop up a message with a list of the new parts collected
    // NVM: dont think this is currently possible because have no way to send list of new parts from PartsCollector to here
    public void OnNotify_EquipParts(int[] weaponParts)
    {

    }

    public void OnNotify_Unequip()
    {
        Debug.Log("Hello from OnNotify_Unequip");

        ResetReticle();
        DeactivateWeaponIcons();
    }

    public void OnNotify_Shoot()
    {
        currMag--;

        ammo.text = currMag + " / " + currInventory;
    }

    public void OnNotify_ShootRecoil(int stability, int recoilControl, WeaponType firingType, bool isADS){}

    public void OnNotify_Reload(StatSheet stats)
    {
        SetAmmoCounts(stats);
    }

    //FINISH WORKING ON THE FADING RETICLE ON ZOOM IN

    public void OnNotify_ZoomIn(int zoom, float adsSpeed)
    {
        // float newAlpha = instWeaponReticle.GetComponent<CanvasGroup>().alpha;
        // newAlpha = Mathf.Lerp(newAlpha, 0, adsSpeed * 2);

        // instWeaponReticle.GetComponent<CanvasGroup>().alpha = newAlpha;
    }

    public void OnNotify_ZoomOut(float adsSpeed)
    {
        // Debug.Log("HELLO");
        // float newAlpha = instWeaponReticle.GetComponent<CanvasGroup>().alpha;
        // newAlpha = Mathf.Lerp(newAlpha, 1, adsSpeed * 0.5f);

        // instWeaponReticle.GetComponent<CanvasGroup>().alpha = newAlpha;
    }
}
