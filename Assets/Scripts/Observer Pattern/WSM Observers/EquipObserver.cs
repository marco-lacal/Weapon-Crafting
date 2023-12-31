public interface EquipObserver : WSMObserver
{
    //send the statssheet so that different uis and things can know mag size
    //and for the partscollector to know what parts are used
    public void OnNotify_Equip(StatSheet stats);

    // Used for PartsCollector
    public void OnNotify_EquipParts(int[] weaponParts, int weaponType);

    public void OnNotify_Unequip();
}
