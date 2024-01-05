using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : Interactable
{
    [SerializeField] private GameObject craftingMenu;

    [Header("Transform that the weaponpickup will be made a child of")]
    [SerializeField] private Transform weaponPickupLocation;

    // private GameObject instCraftingMenu;

    private Transform canvasRef;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void DisplayPrompt(Transform canvas)
    {
        base.DisplayPrompt(canvas);

        if(canvasRef == null)
        {
            canvasRef = canvas;
        }
    }

    public override void Interact(Transform temp)
    {
        base.Interact(temp);

        // if(instCraftingMenu == null)
        // {
        //     //create instance of the crafting ui
        //     instCraftingMenu = GameObject.Instantiate(craftingMenu, canvasRef);
        // }
        // else
        // {
        //     instCraftingMenu.SetActive(true);
        // }

        // Send the craftingbench transform so that the crafting screens can have a reference that is passed up to it to attach the 
        ScreenManager.Instance.CallerObject = weaponPickupLocation;

        ScreenManager.Instance.Push(craftingMenu);

        hasBeenInteractedWith = false;
    }
}
