using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//just gonna use a singleton for now because trying to figure out another way with events and delegates is making my head spin and i want to have something done
public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance {get; private set;}
    public PartsCollector parts {get{return pC;}}

    //delete these

    public Transform WSM {get {return weaponHolder;}}

    //Have a reference to the always persistent WeaponHolder. Need to get two things from the WeaponHolder: WSMSubject for events; equippedWeapon from WSM for the screens
    [SerializeField] private Transform weaponHolder;
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
            Debug.Log(currScreen.name);
            Destroy(currScreen);
        }

        stack.Push(newScreen);
        Debug.Log("Stack count: " + stack.Count);

        if(Time.timeScale != 0f && stack.Count >= 2)
        {
            Debug.Log("Pause game");
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        Debug.Log("Stack count: " + stack.Count);
        currScreen = Instantiate(stack.Peek(), transform);

        if(stack.Count == 1)
        {
            Debug.Log("Unpause game");
            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
