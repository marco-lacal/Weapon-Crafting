// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class testingscope : MonoBehaviour
// {
//     public Camera weaponCam;

//     private Transform reticle;
//     private Transform weapon;
//     private Vector3 localPosition;

//     private Vector3 scopePosition;
//     private Vector3 direction;
//     private Vector3 appliedDirection;

//     //Implement the weapon state machine
//     //Add a new state for ADS and create connections to the other
//     /*
//         HipFire         ADS     

//         HFShoot     ADSShoot      Reload
//     */

//     void Update()
//     {
//         Vector3 baseLocation;

//         //for when the player will start to move around
//         if(weapon != null)
//         {
//             baseLocation = localPosition + transform.localPosition;
//             DetermineADSPosition();
//         }
//         else
//         {
//             return;
//         }

//         if(Input.GetMouseButton(1))
//         {
//             weapon.position = Vector3.Lerp(weapon.position, appliedDirection, 0.02f);
//             weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, 30, 0.02f);
//             transform.GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetComponent<Camera>().fieldOfView, 30, 0.02f);
//         }
//         else if(weapon.localPosition != baseLocation)
//         {
//             //use localPosition because we want to set 
//             weapon.localPosition = Vector3.Lerp(weapon.localPosition, baseLocation, 0.02f);
//             weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, 60, 0.02f);
//             transform.GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetComponent<Camera>().fieldOfView, 60, 0.02f);
//         }
//     }

//     public void FindReticle(GameObject newWeapon)
//     {
//         weapon = newWeapon.transform;

//         Debug.Log(weapon.name);

//         weaponCam.fieldOfView = 60;
//         transform.GetComponent<Camera>().fieldOfView = 60;

//         localPosition = weapon.localPosition;

//         reticle = weapon.Find("A_Scope").Find("Reticle");

//         Debug.Log(reticle);

//         DetermineADSPosition();
//     }

//     public void DetermineADSPosition()
//     {
//         scopePosition = transform.position + transform.forward;

//         Vector3 direction = scopePosition - reticle.position;
//         appliedDirection = weapon.position + direction;
//     }

// }

//FINISH WORKING ON THIS AND GET IT TO USE LOCAL POSITION SO I CAN CALCULATE ONCE AND THATS IT
//NO MORE NEED FOR CALCULATIONS IN THE UPDATE METHOD

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingscope : MonoBehaviour
{
    public Camera weaponCam;

    private Transform reticle;
    private Transform weapon;
    private Vector3 localPosition;
    private Vector3 direction;
    private Vector3 appliedDirection;

    //Implement the weapon state machine
    //Add a new state for ADS and create connections to the other
    /*
        HipFire         ADS     

        HFShoot     ADSShoot      Reload
    */

    void Update()
    {
        //for when the player will start to move around
        // if(weapon != null)
        // {
        //     baseLocation = localPosition + transform.localPosition;
        //     DetermineADSPosition();
        // }
        // else
        // {
        //     return;
        // }

        if(weapon == null)
        {
            return;
        }

        if(Input.GetMouseButton(1))
        {
            weapon.localPosition = Vector3.Lerp(weapon.localPosition, appliedDirection, 0.02f);
            weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, 30, 0.02f);
            transform.GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetComponent<Camera>().fieldOfView, 30, 0.02f);
        }
        else if(weapon.localPosition != localPosition)
        {
            //use localPosition because we want to set 
            weapon.localPosition = Vector3.Lerp(weapon.localPosition, localPosition, 0.02f);
            weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, 60, 0.02f);
            transform.GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetComponent<Camera>().fieldOfView, 60, 0.02f);
        }
    }

    public void FindReticle(GameObject newWeapon)
    {
        weapon = newWeapon.transform;

        weaponCam.fieldOfView = 60;
        transform.GetComponent<Camera>().fieldOfView = 60;

        localPosition = weapon.localPosition;

        reticle = weapon.Find("A_Scope").Find("Reticle");

        DetermineADSPosition();
    }

    public void DetermineADSPosition()
    {
        Vector3 scopePosition = transform.forward + transform.position;

        Vector3 localScope = transform.InverseTransformPoint(scopePosition);
        Vector3 temp = transform.InverseTransformPoint(reticle.position);

        Vector3 localVector = localScope - temp;

        Debug.Log(localVector);

        appliedDirection = localPosition + localVector;
    }

}