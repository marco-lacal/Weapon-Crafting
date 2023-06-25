using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : WeaponPartBase
{
    public Transform Reticle {get => reticle;}

    [SerializeField] private Transform reticle;
}
