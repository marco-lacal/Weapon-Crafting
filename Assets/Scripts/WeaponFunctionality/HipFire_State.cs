using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hipfire Idle State

public class HipFire_State : WeaponBaseState
{
    private Transform weapon;
    private Vector3 HF;
    private float lerpPercentage;

    public override void EnterState(WeaponStateManager WSM)
    {
        //Debug.Log("Hello from the Hipfire idle state");

        if(weapon != WSM.EquippedWeapon || weapon == null)
        {
            weapon = WSM.EquippedWeapon;

            HF = WSM.HF;
        }
    }

    public override void ExitState(WeaponStateManager WSM)
    {
        //Debug.Log("Goodbye HipFire State");
    }

    public override void UpdateState(WeaponStateManager WSM)
    {
        weapon.localPosition = Vector3.Lerp(weapon.localPosition, HF, WSM.ADSSpeed);
        WSM.Notify(ZoomAction.Out, WSM.Stats.ZoomFactor, WSM.ADSSpeed);
        lerpPercentage = weapon.localPosition.magnitude / HF.magnitude;
        // Debug.Log("0.98f " + (lerpPercentage > 0.98f));
        // Debug.Log("0.995f " + (lerpPercentage > 0.995f));

        //Drop weapon on ground
        if((lerpPercentage >= 0.99f) && Input.GetKeyDown(KeyCode.G))
        {
            WSM.DropWeapon();

            weapon = null;
        }

        //Transition to ADS_State
        if(Input.GetMouseButtonDown(1))
        {
            WSM.SwitchState(WSM.ADS);
        }

        //Transition to HipFireShoot_State
        else if(Input.GetMouseButtonDown(0) && WSM.Stats.PracticalMagazine > 0)
        {
            WSM.SwitchState(WSM.HipFireShoot);
        }

        //Transition to Reload_State
        else if(WSM.Stats.InventorySize != 0 && (lerpPercentage >= 0.97f) && ((Input.GetKeyDown(KeyCode.R) && WSM.Stats.Magazine > WSM.Stats.PracticalMagazine) || WSM.Stats.PracticalMagazine == 0))
        {
            WSM.SwitchState(WSM.Reload);
        }
    }
}
