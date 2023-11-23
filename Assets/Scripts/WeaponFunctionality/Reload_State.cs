using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload_State : WeaponBaseState
{
    private float reloadSpeed;

    private float endOfReload;
    private float currentTimer;

    public override void EnterState(WeaponStateManager WSM)
    {
        Debug.Log("Hello from reload state");

        if(reloadSpeed != WSM.Stats.ReloadSpeed)
        {
            reloadSpeed = 4 - ((float)WSM.Stats.ReloadSpeed/100 * 3);

            currentTimer = 0;
        }
    }

    public override void ExitState(WeaponStateManager WSM)
    {
        WSM.RefillMagazine();
        Debug.Log("Goodbye from ReloadState");
    }

    public override void UpdateState(WeaponStateManager WSM)
    {
        WSM.EquippedWeapon.localPosition = Vector3.Lerp(WSM.EquippedWeapon.localPosition, WSM.HF, WSM.ADSSpeed);
        WSM.ZoomOut(WSM.ADSSpeed);
        currentTimer += Time.deltaTime;
        //Debug.Log(currentTimer);

        Mathf.Clamp(currentTimer, 0, reloadSpeed);

        if(currentTimer < reloadSpeed)
        {
            return;
        }

        //Transition to HipFire_State
        WSM.SwitchState(WSM.HipFire);
    }
}
