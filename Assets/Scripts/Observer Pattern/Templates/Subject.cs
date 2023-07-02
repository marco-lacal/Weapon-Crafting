using System.Collections.Generic;
using UnityEngine;

//applied onto the WSM script
public abstract class Subject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    //This function is called the CameraObserver to add itself to the observers List
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    //This function is called the CameraObserver to remove itself from the observers List
    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    //never used
    protected void NotifyObservers()
    {
        observers.ForEach((observer) => {
            observer.OnNotify();
        });
    }

    //IS USED: For the camera Subject-Observers
    protected void NotifyObservers(ZoomAction type, int zoom, float adsSpeed)
    {
        observers.ForEach((observer) => {
            observer.OnNotify(type, zoom, adsSpeed);
        });
    }
}