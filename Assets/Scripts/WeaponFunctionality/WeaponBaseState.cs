using UnityEngine;

public abstract class WeaponBaseState
{
    public abstract void EnterState(WeaponStateManager WSM);

    public abstract void UpdateState(WeaponStateManager WSM);

    public abstract void ExitState(WeaponStateManager WSM);
}