using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : Interactable
{
    [SerializeField] private GameObject craftingMenu;

    private GameObject instCraftingMenu;

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
        Debug.Log(temp);

        base.Interact(temp);

        //create instance of the crafting ui
        instCraftingMenu = GameObject.Instantiate(craftingMenu, canvasRef);

        hasBeenInteractedWith = false;
    }
}
