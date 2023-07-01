using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WSS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] numberStuff;
    [SerializeField] private Transform[] fillBars;

    public void CreateStatSheet(StatSheet stats)
    {
        numberStuff[0].text = stats.Name;
        numberStuff[1].text = stats.RateOfFire.ToString();
        numberStuff[2].text = stats.PracticalMagazine.ToString() + "/" + stats.Magazine.ToString();
        numberStuff[3].text = stats.InventorySize.ToString();

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

        fillBars[0].localScale = new Vector3((float)(stats.Damage)/100, 1, 1);
        fillBars[1].localScale = new Vector3((float)(stats.Range)/100, 1, 1);
        fillBars[2].localScale = new Vector3((float)(stats.Stability)/100, 1, 1);
        fillBars[3].localScale = new Vector3((float)(stats.RecoilControl)/100, 1, 1);
        fillBars[4].localScale = new Vector3((float)(stats.Handling)/100, 1, 1);
        fillBars[5].localScale = new Vector3((float)(stats.ReloadSpeed)/100, 1, 1);
        fillBars[6].localScale = new Vector3((float)(stats.ZoomFactor)/100, 1, 1);
    }
}