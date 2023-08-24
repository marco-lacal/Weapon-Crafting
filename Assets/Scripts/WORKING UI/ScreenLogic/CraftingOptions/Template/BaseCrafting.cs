using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCrafting : MonoBehaviour
{
    [Header("Use appropriate prefabs")]
    [SerializeField] private GameObject PartsCollectionBox;
    [SerializeField] private UnityEvent<int[][]> SetCollection;

    [SerializeField] private int weaponTypeID;

    private int[][] partsCollection;

    void Awake()
    {
        //get partsCollection from somewhere in the game
        //create a random one here for testing
        if(partsCollection == null)
        {
            partsCollection = ScreenManager.Instance.parts.AllParts[weaponTypeID];

            SetCollection?.Invoke(partsCollection);
        }
    }

    public void OnHelpIconClicked()
    {
        Debug.Log("hello");

        PartsCollectionBox.gameObject.SetActive(true);
    }

    public void Cancel()
    {
        ScreenManager.Instance.PopScreen();
    }
}
