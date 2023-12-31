using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionsManager : MonoBehaviour
{
    // To get the correct number of parts for the type of weapon this script is dealing with
    [SerializeField] private int weaponType;

    [SerializeField] private TMP_Dropdown[] dropdowns;
    [SerializeField] private TextMeshProUGUI[] numberTexts;

    // This array will store which weapon each part is taken from 
    // (if selectedParts[0] == 0, Mozark Barrel)
    // If there is a negative value in the array, user picked a part that hasn't been unlocked yet (or none selected yet)
    private int[] selectedParts;

    private int numValidParts;

    public delegate void ChangeDropdownValue(int[] parts, bool isCompleteWeapon);
    public static event ChangeDropdownValue InformOtherComponents;

    void Start()
    {
        int numParts = ScreenManager.Instance.parts.GetNumberOfParts(weaponType);
        int numWeapons = ScreenManager.Instance.parts.GetNumberOfEachWeapon(weaponType);
        selectedParts = new int[numParts];

        for(int i = 0; i < numParts; i++)
        {
            dropdowns[i].value = numWeapons;    // start at the last option which is blank
            numberTexts[i].text = "0";  // blank
            selectedParts[i] = -1;  // set every value to -1 to start
        }

        numValidParts = 0;
        SetUnlockedParts(numParts, numWeapons);
    }

    public void SetUnlockedParts(int numParts, int numWeapons)
    {
        int[][] partsArray = ScreenManager.Instance.parts.GetPartsArray(weaponType);

        Debug.Log(partsArray.Length + "   " + partsArray[0].Length);

        for(int i = 0; i < partsArray.Length - 1; i++)
        {
            for(int j = 0; j < partsArray[0].Length; j++)
            {
                if(partsArray[i][j] == 0)
                {
                    dropdowns[i].options[j].text = "?";
                }
            }
        }
    }

    public void NewChoice(int index)
    {
        // Debug.Log(dropdowns[index].options[dropdowns[index].value].text);

        if(dropdowns[index].options[dropdowns[index].value].text.Equals("?"))
        {
            numberTexts[index].text = "?";
            selectedParts[index] = -1;

            numValidParts--;
        }
        else
        {
            numberTexts[index].text = (dropdowns[index].value + 1).ToString();
            selectedParts[index] = dropdowns[index].value;
            numValidParts++;
        }

        // TESTING PURPOSES
        // string temp = "";

        // for(int i = 0; i < selectedParts.Length; i++)
        // {
        //     temp += selectedParts[i] + " ";
        // }

        // Debug.Log("SelectionsManager array: " + temp);

        InformOtherComponents(selectedParts, numValidParts == 6);
    }
    
}
