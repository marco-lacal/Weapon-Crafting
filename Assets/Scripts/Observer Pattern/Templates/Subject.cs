using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

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

    //For the camera Subject-Observers
    protected void NotifyObservers(ZoomAction type, int zoom, float adsSpeed)
    {
        observers.ForEach((observer) => {
            observer.OnNotify(type, zoom, adsSpeed);
        });
    }
}