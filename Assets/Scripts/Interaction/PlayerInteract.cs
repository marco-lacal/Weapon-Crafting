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
        if(Time.timeScale == 0f)
        {
            return;
        }

        //raycast that will only collide with interactable objects on the eighth layer
        if(Physics.Raycast(transform.position, transform.forward, out hit, 3f, 1 << 8))
        {
            /*
                with the current implementation of the the Physics.Raycast, there was a bug where the raycast would hit two different
                Interactable objects and cause the prompts to be ontop of each other.
            */
            
            //to address the above bug, check if the current foundInteractable script belongs to the new found hit object 
            //and confirm that foundInteractable isnt null. if true, then delete the old foundInteractable and make way for the new one
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