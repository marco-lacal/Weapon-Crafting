using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObserver : MonoBehaviour, IObserver
{
    [SerializeField] private Subject WSM;
    private Camera cameraRef;

    public void OnNotify()
    {
        cameraRef.fieldOfView = 60;
    }

    public void OnNotify(ZoomAction type, int zoomFactor, float adsSpeed)
    {
        int zoomIn = 60 - zoomFactor;

        //will replace the 0.02f's with handling eventually
        if(type == ZoomAction.In)
        {
            cameraRef.fieldOfView = Mathf.Lerp(cameraRef.fieldOfView, zoomIn, adsSpeed);
        }
        else if(type == ZoomAction.Out)
        {
            cameraRef.fieldOfView = Mathf.Lerp(cameraRef.fieldOfView, 60, adsSpeed);
        }
    }

    private void OnEnable()
    {
        //add itself to the Subject's list of observers instead of the other way around
        WSM.AddObserver(this);
        cameraRef = transform.GetComponent<Camera>();
    }

    private void OnDisable()
    {
        WSM.RemoveObserver(this);
    }
}
