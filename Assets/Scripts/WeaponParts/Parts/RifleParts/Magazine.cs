using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : WeaponPartBase
{
    public GameObject BulletHole {get {return bulletHole;}}

    [SerializeField] private GameObject bulletHole;
}
