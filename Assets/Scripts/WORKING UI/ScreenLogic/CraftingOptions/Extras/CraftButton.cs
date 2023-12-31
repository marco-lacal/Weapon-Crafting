using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour, CreateWeaponInterface
{
    [SerializeField] private GameObject notCraftableIcon;

    private int weaponType;
    private int[] weaponParts;

    private bool craftable;

    public event CreateWeaponInterface.CreateWeapon CreateWeaponEvent;

    void Awake()
    {
        SelectionsManager.InformOtherComponents += SetParts;
        weaponType = transform.parent.GetComponent<BaseCrafting>().WeaponTypeID;
        craftable = false;
    }

    public void SetParts(int[] parts, bool isCompleteWeapon)
    {
        weaponParts = parts;
        craftable = isCompleteWeapon;
    }

    // giving error for some reason - craftable = true somehow
    public void OnCraftButtonClicked()
    {
        if(craftable)
        {
            CreateWeaponEvent(weaponType, weaponParts);
        }
        else
        {
            notCraftableIcon.SetActive(true);
        }
    }

}
