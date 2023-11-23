public interface ShootObserver : WSMObserver
{
    public void OnNotify_Shoot();

    public void OnNotify_ShootRecoil(int stability, int recoilControl, WeaponType firingType, bool isADS);
}
