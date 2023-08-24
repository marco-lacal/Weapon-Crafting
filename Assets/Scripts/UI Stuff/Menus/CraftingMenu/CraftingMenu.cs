// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// /*
//     GOING TO TAKE A HIATUS FROM THIS PROJECT BRANCH

//     I am realizing I should do a smaller scale learning project where I create a simplified but still semi-similar
//     project to this task. I.E., making a small demo where its all mouse interactions through button presses, and creating
//     new things based on designated settings
// */

// enum CraftingStates
// {
//     CraftMenu,
//     Craft,
//     Reforge
// }

// public class CraftingMenu : MonoBehaviour
// {
//     [SerializeField] private GameObject craft;
//     [SerializeField] private GameObject reforge;

//     private bool isPaused;

//     //from the WSM script. Used to check before reforge option
//     private Transform EquippedWeapon;

//     private CraftingStates currEnum;

//     void Awake()
//     {
//         currEnum = CraftingStates.CraftMenu;

//         isPaused = false;

//         if(EquippedWeapon == null)
//         {
//             //start search at CameraHolder. will iteratively check for the WSM object starting here
//             Transform search = GameObject.Find("CameraHolder").transform;

//             FindWSMRecursively(search);
//         }
//     }

//     void FindWSMRecursively(Transform obj)
//     {
//         foreach(Transform child in obj)
//         {
//             if(child.GetComponent<WeaponStateManager>() != null)
//             {
//                 EquippedWeapon = child.GetComponent<WeaponStateManager>().EquippedWeapon;

//                 return;
//             }

//             FindWSMRecursively(child);
//         }
//     }

//     void Update()
//     {
//         if(Input.GetKeyDown(KeyCode.Escape))
//         {
//             switch(currEnum)
//             {
//                 case CraftingStates.CraftMenu:
//                     OnDisable();
//                     break;
//                 case CraftingStates.Craft:
//                     break;
//                 case CraftingStates.Reforge:
//                     break;
//                 default:
//                     break;
//             }
//         }
//     }

//     public void DestroySelf()
//     {
//         Destroy(transform.gameObject);
//     }

//     public void OnEnable()
//     {
//         if(!isPaused)
//         {
//             Time.timeScale = 0f;
//             Debug.Log("game paused   " + transform);
//             isPaused = true;

//             Cursor.lockState = CursorLockMode.None;
//             Cursor.visible = true;
//         }
//     }

//     //OnDisable will be called when this gameObject is destroyed
//     public void OnDisable()
//     {
//         if(isPaused)
//         {
//             Time.timeScale = 1f;
//             Debug.Log("game Unpaused   " + transform);
//             isPaused = false;

//             Cursor.lockState = CursorLockMode.Locked;
//             Cursor.visible = false;

//             transform.gameObject.SetActive(false);
//         }
//     }

//     public void SelectCrafting()
//     {

//     }

//     public void SelectReforge()
//     {
//         if(EquippedWeapon != null)
//         {
//             Debug.Log("Reforging weapon");


//         }
//         else
//         {
//             Debug.Log("No weapon");
//         }
//     }
// }
