using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    private Vector3 finalRotation;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private float xRecoil;
    private float yRecoil;

    //Figure out good values for each of these to replace the static interpolation values in the Update() lerps
    private float stabization;     //determines how quickly the weapon returns to (0, 0, 0): higher = near instant; lower = slow
    private float kickStrength;     //determine the kick strength of the weapon: higher = very jumpy; lower = smooth

    void Awake()
    {
        targetRotation = currentRotation = finalRotation = Vector3.zero;
    }

    void OnEnable()
    {
        RecoilEventManager.Recoil += RecoilFunction;
    }

    void OnDisable()
    {
        RecoilEventManager.Recoil -= RecoilFunction;
    }

    public void RecoilFunction(StatSheet stats, WeaponType firingType, bool isADS, Vector3 starting)
    {
        // if(stats != currStats)
        // {
        //     Debug.Log("New stats");

        //     currStats = stats;

        //     float xRecoil = 100 / stats.RecoilControl;
        //     float yRecoil = 100 / stats.Stability;
        // }

        // float xRecoil = 300 / stats.RecoilControl;
        // float yRecoil = 300 / stats.Stability;

        if(finalRotation == Vector3.zero)
        {
            //Debug.Log("starting " + starting);
            finalRotation = starting;
        }

        //if the weapon is in ADSShoot, then decrease all recoil by some scalar.
        float isADSScalar = isADS ? 0.75f: 1;


        switch(firingType)
        {
            case WeaponType.FullAuto:
                xRecoil = (200 / stats.RecoilControl) * isADSScalar;
                yRecoil = (200 / stats.Stability) * isADSScalar;

                stabization = 0.007f;
                kickStrength = 0.02f;
                break;
            case WeaponType.BurstFire:
                xRecoil = (250 / stats.RecoilControl) * isADSScalar;
                yRecoil = (150 / stats.Stability) * isADSScalar;

                stabization = 0.01f;
                kickStrength = 0.03f;
                break;
            case WeaponType.SemiAuto:
                xRecoil = (300 / stats.RecoilControl) * isADSScalar;
                yRecoil = (150 / stats.Stability) * isADSScalar;

                stabization = 0.005f;
                kickStrength = 0.05f;
                break;
            default:
                Debug.Log("Big error");
                xRecoil = yRecoil = 10;
                break;
        }

        targetRotation += new Vector3(-xRecoil, Random.Range(-yRecoil, yRecoil), 0);
        //Debug.Log(transform.eulerAngles);
    }

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, stabization);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, kickStrength);
        transform.localRotation = Quaternion.Euler(currentRotation);

        if(transform.localRotation.eulerAngles == Vector3.zero)
        {
            finalRotation = Vector3.zero;
        }

        // if(transform.localRotation.eulerAngles.x >= finalRotation.x && finalRotation != Vector3.zero)
        // {
        //     Debug.Log("gone below");

        //     finalRotation = Vector3.zero;
        //     targetRotation = new Vector3(0, targetRotation.y, targetRotation.z);
        // }
    }
}
