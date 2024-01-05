using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// Will be accessed directly by PartHolder
public class Viewport : MonoBehaviour, IDragHandler
{
    [SerializeField] private Transform weaponDisplay;
    [SerializeField] private GameObject[] exampleParts;
    private GameObject[] currentPartsInUse;
    private int weaponType;

    private float previousXPosition = 0f;

    void Awake()
    {
        weaponType = transform.parent.GetComponent<BaseCrafting>().WeaponTypeID;

        currentPartsInUse = new GameObject[exampleParts.Length];

        CreateExampleModel();
    }

    public void CreateExampleModel()
    {
        for(int i = 0; i < exampleParts.Length; i++)
        {
            currentPartsInUse[i] = Instantiate(exampleParts[i], weaponDisplay);
        }

        Body curr = currentPartsInUse[1].GetComponent<Body>();

        SetPartsToBody(curr);
    }

    public void SetPartsToBody(Body curr)
    {
        SetBarrelToBody(curr);

        SetGripToBody(curr);
        
        SetMagazineToBody(curr);

        SetScopeToBody(curr);

        SetStockToBody(curr);
    }

    public void SetBarrelToBody(Body curr)
    {
        currentPartsInUse[0].transform.position = curr.BarrelToBody.position;
    }

    public void SetGripToBody(Body curr)
    {
        currentPartsInUse[2].transform.position = curr.GripToBody.position;
    }

    public void SetMagazineToBody(Body curr)
    {
        currentPartsInUse[3].transform.position = curr.MagazineToBody.position;
        currentPartsInUse[3].transform.localRotation = curr.MagazineToBody.localRotation;
    }

    public void SetScopeToBody(Body curr)
    {
        currentPartsInUse[4].transform.position = curr.ScopeToBody.position;
    }

    public void SetStockToBody(Body curr)
    {
        currentPartsInUse[5].transform.position = curr.StockToBody.position;
    }

    public void Make3DModel(int partType, GameObject newPart)
    {
        Destroy(currentPartsInUse[partType]);

        if(newPart == null)
        {
            currentPartsInUse[partType] = Instantiate(exampleParts[partType], weaponDisplay);
        }
        else
        {
            currentPartsInUse[partType] = Instantiate(newPart, weaponDisplay);
        }

        Body curr = currentPartsInUse[1].GetComponent<Body>();

        switch(partType)
        {
            case 0:
                SetBarrelToBody(curr);
                break;
            case 1:
                currentPartsInUse[partType].transform.localPosition = new Vector3(0, 0, 0);
                SetPartsToBody(curr);
                break;
            case 2:
                SetGripToBody(curr);
                break;
            case 3:
                SetMagazineToBody(curr);
                break;
            case 4:
                SetScopeToBody(curr);
                break;
            case 5:
                SetStockToBody(curr);
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        float direction = Math.Clamp(Input.mousePosition.x - previousXPosition, -5, 5);
        weaponDisplay.eulerAngles -= new Vector3(0, direction, 0);

        previousXPosition = Input.mousePosition.x;
    }
}
