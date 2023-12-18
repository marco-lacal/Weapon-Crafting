using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoilObserver : MonoBehaviour, ShootObserver, EquipObserver
{

    private float kickStrength;
    private float stabilization;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    // Unnecessary. Should only be added to the list of ShootObservers if we have a gun

    // void OnEnable()
    // {
    //     if(this != null && ScreenManager.Instance != null)
    //     {
    //         Debug.Log("HEllo");
    //         ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddSObserver((ShootObserver)this);
    //     }
    // }

    void OnDisable()
    {
        OnNotify_Unequip();
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveSObserver((ShootObserver)this);
    }

    void Start()
    {
        if(this != null && ScreenManager.Instance.WSM != null)
        {
            ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddEObserver((EquipObserver)this);
        }
    }

    // CHANGE THIS TO SOMETHING BETTER

    public void OnNotify_ShootRecoil(int stability, int recoilControl, WeaponType firingType, bool isADS)
    {
        Debug.Log("SUCCESS!");

        float isADSScalar = isADS ? 0.75f: 1;

        float xRecoil;
        float yRecoil;

        switch(firingType)
        {
            case WeaponType.FullAuto:
                xRecoil = (200 / recoilControl) * isADSScalar;
                yRecoil = (200 / stability) * isADSScalar;

                stabilization = 0.007f;
                kickStrength = 0.02f;
                break;
            case WeaponType.BurstFire:
                xRecoil = (250 / recoilControl) * isADSScalar;
                yRecoil = (150 / stability) * isADSScalar;

                stabilization = 0.01f;
                kickStrength = 0.03f;
                break;
            case WeaponType.SemiAuto:
                xRecoil = (300 / recoilControl) * isADSScalar;
                yRecoil = (150 / stability) * isADSScalar;

                stabilization = 0.005f;
                kickStrength = 0.05f;
                break;
            default:
                Debug.Log("Big error");
                xRecoil = yRecoil = 10;
                break;
        }

        targetRotation += new Vector3(-xRecoil, Random.Range(-yRecoil, yRecoil), 0);
    }

    public void OnNotify_Shoot(){}

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, stabilization);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, kickStrength);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void OnNotify_Equip(StatSheet stats)
    {
        Debug.Log("Hello again");
        ScreenManager.Instance.WSM.GetComponent<WSMSubject>().AddSObserver((ShootObserver)this);
    }

    public void OnNotify_EquipParts(int[] weaponParts, int weaponType){}

    public void OnNotify_Unequip()
    {
        if(this != null && ScreenManager.Instance.WSM != null)
        {
            ScreenManager.Instance.WSM.GetComponent<WSMSubject>().RemoveSObserver((ShootObserver)this);
        }
        else
        {
            Debug.Log(this + " , " + ScreenManager.Instance.WSM);
        }
    }
}
