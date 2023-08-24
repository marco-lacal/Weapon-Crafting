using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//just gonna use a singleton for now because trying to figure out another way with events and delegates is making my head spin and i want to have something done

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance {get; private set;}
    public PartsCollector parts {get{return pC;}}
    public Transform EquippedWeapon {get {return equippedWeapon;}}

    //This will be used when this system is implemented into current Weapon Crafting. Since both screen manager and weapon holder will have to exist in a scene
    // before runtime, i will just give a pointer to the weapon holder then either replace it with the equippedWeapon transform in WSM or reference it through WSM
    [SerializeField] private Transform equippedWeapon;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private PartsCollector pC;

    private Stack<GameObject> stack;
    private GameObject currScreen;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        stack = new Stack<GameObject>();
        
        Push(playerHUD);
    }

    public void Push(GameObject newScreen)
    {
        if(currScreen != null)
        {
            Destroy(currScreen);
        }

        stack.Push(newScreen);

        if(Time.timeScale != 0f && stack.Count >= 2)
        {
            Debug.Log("Pause game");
            Time.timeScale = 0f;
        }

        currScreen = Instantiate(newScreen, transform);
    }

    public void PopScreen()
    {
        if(stack.Count < 1)
        {
            return;
        }

        if(currScreen != null)
        {
            Destroy(currScreen);
        }

        stack.Pop();
        currScreen = Instantiate(stack.Peek(), transform);

        if(stack.Count < 2)
        {
            Debug.Log("Unpause game");
            Time.timeScale = 1f;
        }
    }
}
