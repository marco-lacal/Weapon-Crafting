using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown[] dropdowns;
    [SerializeField] private TextMeshProUGUI[] numberTexts;

    private int weaponType;

    // This array will store which weapon each part is taken from 
    // (if selectedParts[0] == 0, Mozark Barrel)
    // If there is a negative value in the array, user picked a part that hasn't been unlocked yet (or none selected yet)
    private int[] selectedParts;

    // boolean array to keep track of which dropdowns still have the NONE option
    private bool[] hasBeenChanged;

    public delegate void ChangeDropdownValue(int partType, int weaponNumber);
    public static event ChangeDropdownValue InformOtherComponents;

    void Awake()
    {
        BaseCrafting temp = transform.parent.GetComponent<BaseCrafting>();

        weaponType = temp.WeaponTypeID;

        int numParts = temp.NumberOfWeaponParts;
        int numWeapons = temp.NumberOfEachWeapon;
        selectedParts = new int[numParts];
        Array.Fill(selectedParts, -1);

        for(int i = 0; i < numParts; i++)
        {
            dropdowns[i].value = numWeapons;    // start at the last option which is blank
            numberTexts[i].text = "NA";  // blank
            selectedParts[i] = -1;  // set every value to -1 to start
        }

        hasBeenChanged = new bool[numParts];

        SetUnlockedParts(numParts, numWeapons);
    }

    public void SetUnlockedParts(int numParts, int numWeapons)
    {
        int[][] partsArray = ScreenManager.Instance.parts.GetPartsArray(weaponType);

        for(int i = 0; i < partsArray.Length - 1; i++)
        {
            for(int j = 0; j < partsArray[0].Length; j++)
            {
                if(partsArray[i][j] == 0)
                {
                    dropdowns[i].options[j].text = "? - Locked";
                }
            }
        }
    }

    public void NewChoice(int index)
    {
        // Debug.Log(dropdowns[index].options[dropdowns[index].value].text);

        if(hasBeenChanged == null)
        {
            return;
        }

        if(!hasBeenChanged[index])
        {
            hasBeenChanged[index] = !hasBeenChanged[index];
            dropdowns[index].options.RemoveAt(dropdowns[index].options.Count - 1);
        }

        if(dropdowns[index].options[dropdowns[index].value].text[0] == '?')
        {
            numberTexts[index].text = "?";
            selectedParts[index] = -1;
        }
        else
        {
            numberTexts[index].text = (dropdowns[index].value + 1).ToString();
            selectedParts[index] = dropdowns[index].value;
        }

        // TESTING PURPOSES
        // string temp = "";

        // for(int i = 0; i < selectedParts.Length; i++)
        // {
        //     temp += selectedParts[i] + " ";
        // }

        // Debug.Log("SelectionsManager array: " + temp);

        InformOtherComponents(index, selectedParts[index]);
    }
    
}
