using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Will be accessed directly by PartHolder
public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameplate;
    [SerializeField] private Transform[] statBars;
    [SerializeField] private TextMeshProUGUI[] numberStuff;

    private StatSheet[] selectedPartsStats;

    private int weaponType;

    void Awake()
    {
        BaseCrafting temp = transform.parent.GetComponent<BaseCrafting>();

        weaponType = temp.WeaponTypeID;

        selectedPartsStats = new StatSheet[temp.NumberOfWeaponParts];
    }

    public void AddPart(int partType, StatSheet newPart)
    {
        // check if the part were slotting in already has a part there. If so, we want to subtract part
        if(selectedPartsStats[partType] != null)
        {
            SubtractPart(partType);
        }

        newPart.SetStatsListForParts();

        // int indexForNumStuff = 0;
        // for(int i = 0; i < newPart.StatsList.Count; i++)
        // {
        //     int temp = 0;

        //     if(i == 0 || i == 8 || i == 9)
        //     {
        //         Int32.TryParse(numberStuff[indexForNumStuff].text, out temp);
        //         numberStuff[indexForNumStuff].text = 
        //     }
        // }

        // STAT BARS
        for(int i = 0; i < newPart.StatsList.Count - numberStuff.Length; i++)
        {
            if(i != 6)
            {
                statBars[i].localScale = new Vector3((float)(statBars[i].localScale.x + newPart.StatsList[i + 1]/100f), statBars[i].localScale.y, statBars[i].localScale.z);
            }
            else
            {
                statBars[i].localScale = new Vector3((float)(statBars[i].localScale.x + newPart.StatsList[i + 2]/100f), statBars[i].localScale.y, statBars[i].localScale.z);
            }
        }

        // NUMBER STUFF
        numberStuff[0].text = (Int32.Parse(numberStuff[0].text) + newPart.StatsList[0]).ToString();
        numberStuff[1].text = (Int32.Parse(numberStuff[1].text) + newPart.StatsList[7]).ToString();
        numberStuff[2].text = (Int32.Parse(numberStuff[2].text) + newPart.StatsList[9]).ToString();

        selectedPartsStats[partType] = newPart;

        if(partType == 1)
        {
            nameplate.text = newPart.Name;
            Debug.Log(newPart.Name + "   " + nameplate.text);
        }
    }

    public void SubtractPart(int partType)
    {
        if(selectedPartsStats[partType] == null)
        {
            Debug.Log("NOTHING THERE");
            return;
        }

        // STAT BARS
        for(int i = 0; i < selectedPartsStats[partType].StatsList.Count - numberStuff.Length; i++)
        {
            if(i != 6)
            {
                statBars[i].localScale = new Vector3((float)(statBars[i].localScale.x - selectedPartsStats[partType].StatsList[i + 1]/100f), statBars[i].localScale.y, statBars[i].localScale.z);
            }
            else
            {
                statBars[i].localScale = new Vector3((float)(statBars[i].localScale.x - selectedPartsStats[partType].StatsList[i + 2]/100f), statBars[i].localScale.y, statBars[i].localScale.z);
            }
        }

        // NUMBER STUFF
        numberStuff[0].text = (Int32.Parse(numberStuff[0].text) - selectedPartsStats[partType].StatsList[0]).ToString();
        numberStuff[1].text = (Int32.Parse(numberStuff[1].text) - selectedPartsStats[partType].StatsList[7]).ToString();
        numberStuff[2].text = (Int32.Parse(numberStuff[2].text) - selectedPartsStats[partType].StatsList[9]).ToString();

        selectedPartsStats[partType].ClearList();
        selectedPartsStats[partType] = null;
    }
}