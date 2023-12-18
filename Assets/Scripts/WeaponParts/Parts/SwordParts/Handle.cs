using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public Transform PtoH {get {return PommelToHandle;}}
    public Transform GtoH {get {return GuardToHandle;}}

    [SerializeField] private Transform PommelToHandle;
    [SerializeField] private Transform GuardToHandle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
