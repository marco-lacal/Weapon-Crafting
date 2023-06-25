using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStatsSheet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    private Text RoF;
    [SerializeField] private Transform[] fillBars;

    public void CreateStatSheet(StatSheet stats)
    {
        weaponName.text = stats.Name;

        // for(int i = 0; i < values.Length; i++)
        // {
        //     if(i == 0)
        //     {
        //         fillBars[i].GetComponent<TextMeshProUGUI>().text = values[i].ToString();

        //         continue;
        //     }
        //     else if(i == 7)
        //     {
        //         Transform magazine = fillBars[i].GetChild(0);
        //         magazine = magazine.GetChild(0);

        //         magazine.GetComponentInChildren<TextMeshProUGUI>().text = values[i].ToString();

        //         Transform inventory = fillBars[i].GetChild(1);
        //         inventory = inventory.GetChild(0);

        //         inventory.GetComponentInChildren<TextMeshProUGUI>().text = (values[i] * 10).ToString();

        //         continue;
        //     }

        //     fillBars[i].localScale = new Vector3((float)(values[i])/100, 1, 1);
        // }

        
    }
}