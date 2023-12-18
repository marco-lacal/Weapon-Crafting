using System.Collections.Generic;
using UnityEngine;

public abstract class WSMSubject : MonoBehaviour
{
    private List<EquipObserver> equipObservers = new List<EquipObserver>();
    private List<ShootObserver> shootObservers = new List<ShootObserver>();
    private List<ZoomObserver> zoomObservers = new List<ZoomObserver>();
    private List<ReloadObserver> reloadObservers = new List<ReloadObserver>();

    // Add observers to their respective lists
    public void AddEObserver(EquipObserver observer)
    {
        equipObservers.Add(observer);
    }

    public void AddSObserver(ShootObserver observer)
    {
        shootObservers.Add(observer);
    }
    public void AddZObserver(ZoomObserver observer)
    {
        zoomObservers.Add(observer);
    }
    public void AddRObserver(ReloadObserver observer)
    {
        reloadObservers.Add(observer);
    }

    // Remove observers from their respective lists
    public void RemoveEObserver(EquipObserver observer)
    {
        equipObservers.Remove(observer);
    }

    public void RemoveSObserver(ShootObserver observer)
    {
        shootObservers.Remove(observer);
    }

    public void RemoveZObserver(ZoomObserver observer)
    {
        zoomObservers.Remove(observer);
    }

    public void RemoveRObserver(ReloadObserver observer)
    {
        reloadObservers.Remove(observer);
    }

    // EquipObserver Notifications
    protected void Notify_Equip(StatSheet stats)
    {
        equipObservers.ForEach((equipObservers) => {
            equipObservers.OnNotify_Equip(stats);
        });
    }

    protected void Notify_Equip_NewParts(int[] weaponParts, int weaponType)
    {
        equipObservers.ForEach((equipObservers) => {
            equipObservers.OnNotify_EquipParts(weaponParts, weaponType);
        });
    }

    protected void Notify_Unequip()
    {
        equipObservers.ForEach((equipObservers) => {
            equipObservers.OnNotify_Unequip();
        });
    }

    // ShootObserver Notifications
    protected void Notify_Shoot()
    {
        shootObservers.ForEach((shootObservers) => {
            shootObservers.OnNotify_Shoot();
        });
    }

    protected void Notify_ShootRecoil(int stability, int recoilControl, WeaponType firingType, bool isADS)
    {
        shootObservers.ForEach((shootObservers) => {
            shootObservers.OnNotify_ShootRecoil(stability, recoilControl, firingType, isADS);
        });
    }

    // ZoomObserver Notifications
    protected void Notify_ZoomIn(int zoom, float adsSpeed)
    {
        zoomObservers.ForEach((zoomObservers) => {
            zoomObservers.OnNotify_ZoomIn(zoom, adsSpeed);
        });
    }

    protected void Notify_ZoomOut(float adsSpeed)
    {
        zoomObservers.ForEach((zoomObservers) => {
            zoomObservers.OnNotify_ZoomOut(adsSpeed);
        });
    }

    // ReloadObserver Notifications
    protected void Notify_Reload(StatSheet stats)
    {
        reloadObservers.ForEach((reloadObservers) => {
            reloadObservers.OnNotify_Reload(stats);
        });
    }
}
