using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsAttached : MonoBehaviour
{
    [SerializeField] private GameObject partBox;

    private int weaponType;

    private GameObject[] partBoxes;

    void Awake()
    {
        SelectionsManager.InformOtherComponents += AdjustBoxes;

        // child of statsDisplay which is child of BaseCrafting
        BaseCrafting temp = transform.parent.parent.GetComponent<BaseCrafting>();

        weaponType = temp.WeaponTypeID;

        int numParts = temp.NumberOfWeaponParts;

        // scale up the background image
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2((40 * numParts) + 20, 50);

        float xLocation = transform.GetComponent<RectTransform>().sizeDelta.x / 2;

        partBoxes = new GameObject[numParts];

        for(int i = 0; i < numParts; i++)
        {
            partBoxes[i] = Instantiate(partBox, transform);
            partBoxes[i].transform.localPosition = new Vector2((40 * i) + 30 - xLocation, 0);

            Debug.Log(partBoxes[i]);
        }
    }

    void OnDisable()
    {
        SelectionsManager.InformOtherComponents -= AdjustBoxes;
    }

    public void AdjustBoxes(int partType, int weaponNumber)
    {
        Debug.Log(partBoxes[0]);

        GameObject blackBox = partBoxes[partType].transform.GetChild(0).gameObject;

        if(weaponNumber != -1 && !blackBox.activeSelf)
        {
            partBoxes[partType].transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(weaponNumber == -1 && blackBox.activeSelf)
        {
            partBoxes[partType].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
