using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilEventManager : MonoBehaviour
{
    public delegate void DetermineRecoil(StatSheet stats, WeaponType firingType, bool isADS, Vector3 startingEulerAngles);
    public static event DetermineRecoil Recoil;

    public void OnShoot(StatSheet stats, WeaponType firingType, bool isADS, Vector3 startingEulerAngles)
    {
        Recoil(stats, firingType, isADS, startingEulerAngles);
    }
}
