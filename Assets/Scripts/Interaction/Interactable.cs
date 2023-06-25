using UnityEngine;
using UnityEngine.UI;

//abstract class that all gameobjects that can be interacted with will derive from (chests, weapon pickups, etc)
public abstract class Interactable : MonoBehaviour
{
    //interaction prompt - "E: Open"
    public Image prompt;
    //determing if this interacble object is actice
    [HideInInspector] public bool hasBeenInteractedWith;

    private Image instPrompt;

    public virtual void Start()
    {
        //set to false, thus it is active
        hasBeenInteractedWith = false;
    }

    //this function is called whenever the PlayerInteract finds an interactable object
    //will play on every update
    public virtual void DisplayPrompt(Transform canvas)
    {
        if(instPrompt == null)
        {
            instPrompt = Instantiate(prompt, canvas);
            //instPrompt.transform.parent = canvas;
        }
    }

    public virtual void DestroyPrompt()
    {
        if(instPrompt != null)
        {
            Destroy(instPrompt.gameObject);
        }
    }

    public virtual void Interact(Transform temp)
    {
        hasBeenInteractedWith = true;

        DestroyPrompt();

        //so that this interactable object isnt considered anymore
        transform.gameObject.layer = 7;
    }
}