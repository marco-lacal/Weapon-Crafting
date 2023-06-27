using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    /*
        Do not make a reference to the prompt and stat locations on the canvas.
        instead, bake the location into the prefabs and instantiate them at 0, 0 on the canvas
        By doing this, they will default to their prefab locations but still be children of canvas
        No need to do location objects in the canvas like i had before
    */

    //still need a reference to the canvas as that will be the parent
    public Transform canvas;
    [SerializeField] private Transform weaponHolder;

    private RaycastHit hit;
    private Interactable foundInteractable;

    // Update is called once per frame
    void Update()
    {
        //raycast that will only collide with interactable objects on the eighth layer
        if(Physics.Raycast(transform.position, transform.forward, out hit, 3f, 1 << 8))
        {
            if(!GameObject.ReferenceEquals(hit.transform.GetComponent<Interactable>(), foundInteractable) && foundInteractable != null)
            {
                foundInteractable.DestroyPrompt();
                foundInteractable = null;
            }

            foundInteractable = hit.transform.GetComponent<Interactable>();

            if(!foundInteractable.hasBeenInteractedWith)
            {
                foundInteractable.DisplayPrompt(canvas);
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                foundInteractable.Interact(weaponHolder);
            }
        }
        else if(foundInteractable != null)
        {
            foundInteractable.DestroyPrompt();
            foundInteractable = null;
        }
    }
}