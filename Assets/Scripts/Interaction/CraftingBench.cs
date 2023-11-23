using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : Interactable
{
    [SerializeField] private GameObject craftingMenu;

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

        ScreenManager.Instance.Push(craftingMenu);

        hasBeenInteractedWith = false;
    }
}
