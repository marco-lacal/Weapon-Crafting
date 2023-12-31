using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCrafting : MonoBehaviour// , CreateWeaponInterface
{
    public int WeaponTypeID {get {return weaponTypeID;}}

    [Header("Use appropriate prefabs")]
    [SerializeField] private GameObject PartsCollectionBox;
    [SerializeField] private UnityEvent<int[][]> SetCollection;

    [SerializeField] private int weaponTypeID;

    [SerializeField] private GameObject noCraftingIcon;

    private int[][] partsCollection;

    // public event CreateWeaponInterface.CreateWeapon CreateWeaponEvent;

    private Coroutine fading;

    void Awake()
    {
        //get partsCollection from somewhere in the game
        //create a random one here for testing
        if(partsCollection == null)
        {
            partsCollection = ScreenManager.Instance.parts.GetPartsArray(weaponTypeID);

            Debug.Log(partsCollection.Length + "  " + partsCollection[0].Length);

            SetCollection?.Invoke(partsCollection);
        }
    }

    public void OnHelpIconClicked()
    {
        PartsCollectionBox.gameObject.SetActive(true);
    }

    // public void OnCraftButtonClicked(int type, int[] parts)
    // {
    //     for(int i = 0; i < parts.Length; i++)
    //     {
    //         if(parts[i] < 0)
    //         {
    //             //ActivateIcon();
    //             return;
    //         }
    //     }

    //     // CreateWeaponEvent(type, parts);
    // }

    // public void ActivateIcon()
    // {
    //     if(!noCraftingIcon.activeSelf)
    //     {
    //         noCraftingIcon.SetActive(true);
    //     }

    //     if(fading == null)
    //     {
    //         fading = StartCoroutine(Countdown());
    //     }
    //     else
    //     {
    //         StopCoroutine(fading);
    //         fading = StartCoroutine(Countdown());
    //     }
    // }

    // IEnumerator Countdown()
    // {
    //     yield return new WaitForSeconds(2f);

    //     noCraftingIcon.SetActive(false);
    // }

    // public void OnBadParts()
    // {
    //     noCraftingIcon.SetActive(true);
    // }

    public void Cancel()
    {
        ScreenManager.Instance.PopScreen();
    }
}
