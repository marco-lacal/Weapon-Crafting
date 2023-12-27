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

    // 
    private Dictionary<int, Sprite> numberSprites;

    void Start()
    {
        int numParts = ScreenManager.Instance.parts.GetNumberOfParts(weaponType);
        int numWeapons = ScreenManager.Instance.parts.GetNumberOfEachWeapon(weaponType);

        for(int i = 0; i < numParts; i++)
        {
            dropdowns[i].value = numWeapons;
            numberTexts[i].text = "0";
        }
    }

    public void NewChoice(int index)
    {
        Debug.Log("Box " + index + ": " + dropdowns[index].value);

        numberTexts[index].text = (dropdowns[index].value + 1).ToString();
    }
    
}
