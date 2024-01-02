using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftButton : MonoBehaviour, CreateWeaponInterface
{
    [SerializeField] private GameObject notCraftableIcon;

    private int weaponType;
    private int numParts;
    private int[] weaponParts;

    // adds when new piece added and subtracts when piece removed (or unavailable one selected).
    // when this value equals the number of parts for the weapon type, then we can craft
    private int craftableCounter;

    private Coroutine fading;

    public event CreateWeaponInterface.CreateWeapon CreateWeaponEvent;

    void Awake()
    {
        SelectionsManager.InformOtherComponents += SetParts;

        BaseCrafting temp = transform.parent.GetComponent<BaseCrafting>();

        weaponType = temp.WeaponTypeID;

        numParts = temp.NumberOfWeaponParts;
        weaponParts = new int[numParts];
        Array.Fill(weaponParts, -1);

        craftableCounter = 0;
    }

    void OnDisable()
    {
        SelectionsManager.InformOtherComponents -= SetParts;
    }

    public void SetParts(int partType, int weaponNumber)
    {
        Debug.Log(partType + "   " + weaponNumber);

        // if was changed from -1 to valid number, increment
        if(weaponParts[partType] == -1)
        {
            craftableCounter++;
        }

        weaponParts[partType] = weaponNumber;

        // after the change, if it BECAME -1, then decrement
        if(weaponParts[partType] == -1)
        {
            craftableCounter--;
        }

        // Debug.Log(craftableCounter);
    }

    // public void SetParts(int[] parts, bool isCompleteWeapon)
    // {
    //     weaponParts = parts;
    //     craftable = isCompleteWeapon;
    // }

    // giving error for some reason - craftable = true somehow
    public void OnCraftButtonClicked()
    {
        if(craftableCounter == numParts)
        {
            // CreateWeaponEvent(weaponType, weaponParts);
            Debug.Log("YES");
        }
        else
        {
            if(fading != null)
            {
                StopCoroutine(fading);
            }

            notCraftableIcon.SetActive(true);
            StartCoroutine(FadeNoCraft());
        }
    }

    IEnumerator FadeNoCraft()
    {
        yield return new WaitForSecondsRealtime(2f);

        notCraftableIcon.SetActive(false);
    }

}
