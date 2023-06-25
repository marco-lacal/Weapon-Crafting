using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireShoot_State : WeaponBaseState
{
    private Transform weapon;
    private Vector3 HF;

    private float lastShot;

    private Coroutine ass;

    public override void EnterState(WeaponStateManager WSM)
    {
        //Debug.Log("Hello from the HipFireShoot state");

        if(weapon != WSM.EquippedWeapon || weapon == null)
        {
            weapon = WSM.EquippedWeapon;

            HF = WSM.HF;

            //SOME EVENT HERE TO CALL THE RECOIL SCRIPT I WILL MAKE EVENTUALLY
        }
    }

    public override void ExitState(WeaponStateManager WSM)
    {
        //Debug.Log("Stopped shooting");
    }

    public override void UpdateState(WeaponStateManager WSM)
    {
        //the lerping of camera back to correct location
        weapon.localPosition = Vector3.Lerp(weapon.localPosition, HF, WSM.ADSSpeed);
        WSM.Notify(ZoomAction.Out, WSM.Stats.ZoomFactor, WSM.ADSSpeed);

        //Shoot function
        if(WSM.Stats.PracticalMagazine >= 1 && WSM.LastShot <= Time.time)
        {
            switch(WSM.FiringType)
            {
                case WeaponType.FullAuto:
                    Shoot(WSM);
                    break;
                case WeaponType.SemiAuto:
                    Shoot(WSM);
                    WSM.SwitchState(WSM.HipFire);
                    break;
                case WeaponType.BurstFire:
                    if(ass != null)
                    {
                        return;
                    }

                    ass = WSM.StartCoroutine(BurstFireCoroutine(WSM, WSM.BurstSize, WSM.BurstDelay));

                    WSM.SwitchState(WSM.HipFire);
                    break;
                default:
                    break;
            }
        }

        //force the transition to HF then Reload
        if(WSM.Stats.PracticalMagazine == 0)
        {
            WSM.SwitchState(WSM.HipFire);
        }

        //Transition to HipFire_State
        if(Input.GetMouseButtonUp(0))
        {
            WSM.SwitchState(WSM.HipFire);
        }

        //Transition to ADSSHoot_State
        if(Input.GetMouseButtonDown(1))
        {
            WSM.SwitchState(WSM.ADSShoot);
        }
    }

    private void Shoot(WeaponStateManager WSM)
    {
        //Adjust this formula until desired results
        //Debug.Log(WSM.Stats.RateOfFire);

        WSM.PlayGunShot();
        WSM.EquippedWeapon.localPosition -= Vector3.forward * 0.1f; //higher rate of fire should have smaller number
        WSM.AddRecoil(false, WSM.transform.eulerAngles);

        WSM.DetermineNextShot();
        WSM.DecreaseMag();

        RaycastHit hit;
        if(Physics.Raycast(WSM.transform.position, WSM.transform.forward, out hit, 100))
        {
            Debug.Log(hit.transform.gameObject.name);

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
                Shoot(WSM);
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

        ass = null;
    }

    public void DamageCalculation(WeaponStateManager WSM, RaycastHit hit, bool isBody)
    {
        //want to change this when i eventually add enemies. will have the enemy interface have body and crit multipliers that vary per enemy type
        //say one enemy has .75 body but 2.0 crit and another has 1 for both body and crit
        float damageMultiplier = isBody ? 1f : 1.5f;

        float distance = Vector3.Distance(hit.point, WSM.transform.position);

        if(distance <= WSM.Stats.Range)
        {
            Debug.Log("Within range Damage: " + WSM.Stats.Damage);
            
            WSM.ShowDamageNumbers(hit, (WSM.Stats.Damage * damageMultiplier));
        }
        else
        {
            //example: range = 30, distance = 95
            float remainingDistance = 100 - WSM.Stats.Range;    //70
            float distancePastRange = distance - WSM.Stats.Range;   //65

            float unroundedDamage = (WSM.Stats.Damage * ((remainingDistance - distancePastRange) / remainingDistance));

            float damage = Mathf.Round(unroundedDamage * 100.0f) / 100.0f;

            Debug.Log("Outside range Damage: " + damage);

            WSM.ShowDamageNumbers(hit, (damage * damageMultiplier));
        }
    }
}
