using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ADS Idle State

public class ADS_State : WeaponBaseState
{
    private Transform reticle;
    private Transform weapon;
    private Vector3 ADS;

    public override void EnterState(WeaponStateManager WSM)
    {
        //Debug.Log("Hello from ADS state");

        if(reticle != WSM.RetrieveR())
        {
            reticle = WSM.RetrieveR();
            weapon = WSM.EquippedWeapon;
            

            DetermineADSPosition(WSM);
        }
    }

    public override void ExitState(WeaponStateManager WSM)
    {
        //Debug.Log("Goodbye from ADS state");
    }

    public override void UpdateState(WeaponStateManager WSM)
    {
        weapon.localPosition = Vector3.Lerp(weapon.localPosition, ADS, WSM.ADSSpeed);
        WSM.Notify(ZoomAction.In, WSM.Stats.ZoomFactor, WSM.ADSSpeed);
        
        //create events that contact cameras to zoom in

        //Transition to HipFire_State
        if(Input.GetMouseButtonUp(1))
        {
            WSM.SwitchState(WSM.HipFire);
        }

        //Transition to ADSShoot_State
        else if(Input.GetMouseButtonDown(0) && WSM.Stats.PracticalMagazine > 0)
        {
            WSM.SwitchState(WSM.ADSShoot);
        }
    }

    public void DetermineADSPosition(WeaponStateManager WSM)
    {
        Vector3 scopePosition = (WSM.transform.forward / 2) + WSM.transform.position;

        Vector3 localScope = WSM.transform.InverseTransformPoint(scopePosition);
        Vector3 temp = WSM.transform.InverseTransformPoint(reticle.position);

        Vector3 localVector = localScope - temp;

        ADS = WSM.EquippedWeapon.localPosition + localVector;
    }
}
