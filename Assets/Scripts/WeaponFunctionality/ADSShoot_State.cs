using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSShoot_State : WeaponBaseState
{
    private Transform reticle;
    private Transform weapon;
    private Vector3 ADS;

    private float lastShot;

    private Coroutine burstCR;

    public override void EnterState(WeaponStateManager WSM)
    {
        if(reticle != WSM.RetrieveR())
        {
            reticle = WSM.RetrieveR();
            weapon = WSM.EquippedWeapon;

            DetermineADSPosition(WSM);
        }
    }

    public override void ExitState(WeaponStateManager WSM)
    {
        
    }

    public override void UpdateState(WeaponStateManager WSM)
    {
        weapon.localPosition = Vector3.Lerp(weapon.localPosition, ADS, WSM.ADSSpeed);
        WSM.Notify(ZoomAction.In, WSM.Stats.ZoomFactor, WSM.ADSSpeed);

        //Shoot function
        if(WSM.Stats.PracticalMagazine >= 1 && WSM.LastShot <= Time.time)
        {
            switch(WSM.FiringType)
            {
                case WeaponType.FullAuto:
                    ADSShoot(WSM);
                    break;
                case WeaponType.SemiAuto:
                    ADSShoot(WSM);
                    WSM.SwitchState(WSM.ADS);
                    break;
                case WeaponType.BurstFire:
                    if(burstCR != null)
                    {
                        return;
                    }

                    burstCR = WSM.StartCoroutine(BurstFireCoroutine(WSM, WSM.BurstSize, WSM.BurstDelay));

                    WSM.SwitchState(WSM.ADS);
                    break;
                default:
                    break;
            }
        }

        //Transition to ADS_State - only for Full Auto weapons, as the other firingtypes send to idle states
        if(Input.GetMouseButtonUp(0))
        {
            WSM.SwitchState(WSM.ADS);
        }

        //Transition to HipFireShoot_State
        if(Input.GetMouseButtonUp(1))
        {
            WSM.SwitchState(WSM.HipFireShoot);
        }
    }

    public void DetermineADSPosition(WeaponStateManager WSM)
    {
        //this is the desired position of the RETICLE. 
        Vector3 scopePosition = (WSM.transform.forward / 2) + WSM.transform.position;

        //set the desired position (localScope) and reticle's position (temp)
        //to local space since I want to get a localPosition value
        Vector3 localScope = WSM.transform.InverseTransformPoint(scopePosition);
        Vector3 temp = WSM.transform.InverseTransformPoint(reticle.position);

        //perform vector subtraction to determine the
        //offset that I need to move the entire gun by
        Vector3 localVector = localScope - temp;

        //now add that offset, then interpolate towards ADS on each frame
        ADS = WSM.EquippedWeapon.localPosition + localVector;
    }

    //Maybe give this method more stable shooting cuz its ads
    private void ADSShoot(WeaponStateManager WSM)
    {
        WSM.PlayGunShot();
        WSM.EquippedWeapon.localPosition -= Vector3.forward * 0.05f;
        WSM.AddRecoil(true, WSM.transform.eulerAngles);

        WSM.DetermineNextShot();
        WSM.DecreaseMag();

        RaycastHit hit;
        if(Physics.Raycast(WSM.transform.position, WSM.transform.forward, out hit, 100 + (WSM.Stats.ZoomFactor * 0.5f)))
        {
            if(hit.transform.gameObject.tag == "BodyShot")
            {
                //do body shot stuff in function
                DamageCalculation(WSM, hit, true);
            }
            else if(hit.transform.gameObject.tag == "CritShot")
            {
                //do crit shot stuff in same function just change bool
                DamageCalculation(WSM, hit, false);
            }
            else
            {
                WSM.CreateBulletHole(hit);
            }
        }
    }

    IEnumerator BurstFireCoroutine(WeaponStateManager WSM, int shotsPerBurst, float burstDelay)
    {
        for(int i = 0; i < shotsPerBurst; i++)
        {
            if(WSM.Stats.PracticalMagazine > 0)
            {
                ADSShoot(WSM);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(burstDelay);

            if(i == shotsPerBurst - 1)
            {
                yield return new WaitForSeconds(burstDelay);
            }
        }

        burstCR = null;
    }

    //This is currently not working as intended for going past range. will work to fix it soon
    public void DamageCalculation(WeaponStateManager WSM, RaycastHit hit, bool isBody)
    {
        //want to change this when i eventually add enemies. will have the enemy interface have body and crit multipliers that vary per enemy type
        //say one enemy has .75 body but 2.0 crit and another has 1 for both body and crit
        float damageMultiplier = isBody ? 1f : 1.5f;

        float distance = Vector3.Distance(hit.point, WSM.transform.position);

        if(distance <= WSM.Stats.Range + (WSM.Stats.ZoomFactor * 0.5f))
        {
            WSM.ShowDamageNumbers(hit, (WSM.Stats.Damage * damageMultiplier));
        }
        else
        {
            //example: range = 30, distance = 65
            float remainingDistance = 100 - WSM.Stats.Range + (WSM.Stats.ZoomFactor * 0.5f);
            float distancePastRange = distance - (WSM.Stats.Range + (WSM.Stats.ZoomFactor * 0.5f));

            float unroundedDamage = (WSM.Stats.Damage * ((remainingDistance - distancePastRange) / remainingDistance));

            float damage = Mathf.Round(unroundedDamage * 100.0f) / 100.0f;

            //Debug.Log("Range + ZoomFactor: " + (WSM.Stats.Range + (WSM.Stats.ZoomFactor * 0.5f)) + ", Distance: " + distance + ", rD: " + remainingDistance + ", dPR: " + distancePastRange + ", uD: " + unroundedDamage + ", damage: " + damage);

            WSM.ShowDamageNumbers(hit, (damage * damageMultiplier));
        }
    }
}
