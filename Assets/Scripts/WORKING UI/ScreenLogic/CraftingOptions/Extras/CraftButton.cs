using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour, CreateWeaponInterface
{
    [SerializeField] private GameObject notCraftableIcon;
    [SerializeField] private GameObject weaponPickup;

    private int weaponType;
    private int numParts;
    private int[] weaponParts;

    // adds when new piece added and subtracts when piece removed (or unavailable one selected).
    // when this value equals the number of parts for the weapon type, then we can craft
    private int craftableCounter;

    private Coroutine fading;

    private GameObject instantiatedWP;

    public event CreateWeaponInterface.CreateWeapon CreateWeaponEvent;

    void Awake()
    {
        SelectionsManager.InformOtherComponents += SetParts;

        instantiatedWP = Instantiate(weaponPickup, transform);
        instantiatedWP.transform.position = ScreenManager.Instance.CallerObject.position;

        Debug.Log(instantiatedWP.transform.localScale);

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

        // Means that the craft process wasn't successful (canceled early), so delete the unutilized WeaponPickup
        if(instantiatedWP != null)
        {
            Destroy(instantiatedWP);
        }
    }

    public void SetParts(int partType, int weaponNumber)
    {
        Debug.Log(partType + "   " + weaponNumber);

        // if was changed from -1 to valid number, increment
        if(weaponParts[partType] == -1)
        {
            craftableCounter++;
        }

        weaponParts[partType] = weaponNumber + 1;

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
            Debug.Log("YES");

            CreateWeaponEvent(weaponType, weaponParts);

            // Not 100% sure why this happens, but the weaponpickup object shrinks to roughly half the scale as it should be.
            // when first instantiated in Awake(), its normal size, but here it isnt, so need to adjust
            // Could have to do with canvas scale (CraftButton) being different from regular world space (WeaponCrate)
            instantiatedWP.transform.localScale = Vector3.one;

            // null it out to show that it is complete and we no longer need the reference
            instantiatedWP = null;

            ScreenManager.Instance.CraftSuccessful();
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
