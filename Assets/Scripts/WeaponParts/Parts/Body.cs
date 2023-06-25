using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
    {
        FullAuto,
        BurstFire,
        SemiAuto
    }

public class Body : WeaponPartBase
{
    public string Name {get {return weaponName;}}
    public Transform BarrelToBody {get => barrelToBody;}
    public Transform GripToBody {get => gripToBody;}
    public Transform MagazineToBody {get => magazineToBody;}
    public Transform ScopeToBody {get => scopeToBody;}
    public Transform StockToBody {get => stockToBody;}
    public WeaponType FiringType {get {return firingType;}}
    
    public int BurstSize {get {return burstSize;}}
    public float BurstDelay {get {return burstDelay;}}

    [SerializeField] private string weaponName;

    [SerializeField] private Transform barrelToBody;
    [SerializeField] private Transform gripToBody;
    [SerializeField] private Transform magazineToBody;
    [SerializeField] private Transform scopeToBody;
    [SerializeField] private Transform stockToBody;

    [SerializeField] private WeaponType firingType;
    [SerializeField] private int burstSize;
    [SerializeField] private float burstDelay;
}
