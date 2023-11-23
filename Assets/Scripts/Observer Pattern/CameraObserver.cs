using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Both camera objects have this script on it
// public class CameraObserver : MonoBehaviour, IObserver
// {
//     //reference to the Subject WSM so that on enable, we can add and remove this script from the List of observers
//     [SerializeField] private Subject WSM;
//     private Camera cameraRef;

//     //Not used anywhere
//     public void OnNotify()
//     {
//         cameraRef.fieldOfView = 60;
//     }

//     //used to zoom in or out based on ZoomAction (see WeaponStateManager for the enum)
//     public void OnNotify(ZoomAction type, int zoomFactor, float adsSpeed)
//     {
//         int zoomIn = 60 - zoomFactor;

//         if(type == ZoomAction.In)
//         {
//             cameraRef.fieldOfView = Mathf.Lerp(cameraRef.fieldOfView, zoomIn, adsSpeed);
//         }
//         else if(type == ZoomAction.Out)
//         {
//             cameraRef.fieldOfView = Mathf.Lerp(cameraRef.fieldOfView, 60, adsSpeed);
//         }
//     }

//     //add this object to the WSM List when its active
//     private void OnEnable()
//     {
//         //add itself to the Subject's list of observers instead of the other way around
//         WSM.AddObserver(this);
//         cameraRef = transform.GetComponent<Camera>();
//     }

//     //then remove it when its unactive
//     private void OnDisable()
//     {
//         WSM.RemoveObserver(this);
//     }
// }

public class CameraObserver : MonoBehaviour, ZoomObserver
{
    //reference to the Subject WSM so that on enable, we can add and remove this script from the List of observers
    [SerializeField] private WSMSubject WSM;
    private Camera cameraRef;

    private int zoomIn;

    //Not used anywhere
    public void OnNotify_ZoomIn(int zoomFactor, float adsSpeed)
    {
        int zoomIn = 60 - zoomFactor;

        cameraRef.fieldOfView = Mathf.Lerp(cameraRef.fieldOfView, zoomIn, adsSpeed);
    }

    //used to zoom in or out based on ZoomAction (see WeaponStateManager for the enum)
    public void OnNotify_ZoomOut(float adsSpeed)
    {
        cameraRef.fieldOfView = Mathf.Lerp(cameraRef.fieldOfView, 60, adsSpeed);
    }

    //add this object to the WSM List when its active
    private void OnEnable()
    {
        //add itself to the Subject's list of observers instead of the other way around
        WSM.AddZObserver(this);
        cameraRef = transform.GetComponent<Camera>();
    }

    //then remove it when its unactive
    private void OnDisable()
    {
        WSM.RemoveZObserver(this);
    }
}
