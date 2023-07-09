using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTemplate : MonoBehaviour
{
    public static bool isPaused;

    void Awake()
    {
        isPaused = false;
    }

    public void DestroySelf()
    {
        Destroy(transform.gameObject);
    }

    public virtual void OnEnable()
    {
        if(!isPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("game paused   " + transform);
            isPaused = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //OnDisable will be called when this gameObject is destroyed
    public virtual void OnDisable()
    {
        if(isPaused)
        {
            Time.timeScale = 1f;
            Debug.Log("game Unpaused   " + transform);
            isPaused = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
