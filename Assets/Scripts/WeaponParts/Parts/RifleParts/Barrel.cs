using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : WeaponPartBase
{
    public ParticleSystem MuzzleFlash {get => muzzleFlash;}

    [SerializeField] private ParticleSystem muzzleFlash;

    public void PlayMuzzleFlash()
    {

    }
}
