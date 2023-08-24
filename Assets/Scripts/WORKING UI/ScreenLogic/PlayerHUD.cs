using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Next Screen")]
    [SerializeField] private GameObject nextScreen;

    [Header("Reticle")]
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject weaponReticle;   //obtained from the WSM that ScreenManager has

    private GameObject instWeaponReticle;

    [Header("Array of Weapon Icons")]
    [SerializeField] private GameObject[] weaponIcons;

    void Awake()
    {
        //for later
        if(ScreenManager.Instance.EquippedWeapon != null)
        {
            SetWeaponReticle();
            ActivateWeaponIcons();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //equipped weapon
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetWeaponReticle();
            ActivateWeaponIcons();
        }
        else if(Input.GetKeyDown(KeyCode.G))
        {
            ResetReticle();
            DeactivateWeaponIcons();
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            ScreenManager.Instance.Push(nextScreen);
        }
    }

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
}
