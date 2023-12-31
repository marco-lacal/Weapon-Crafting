/*
    This interface is solely for scripts that can create weapons. Those scripts will implement this interface's event
    so that the WeaponPickup does not have to worry about which of potentiall numerous weapon creating scripts is calling it

    Having to check each possible script that has a CreateWeaponEvent

    // if(transform.parent.TryGetComponent<WeaponCrate>(out WeaponCrate crate) == true)    // Created from a WeaponCrate
    // {
    //     crate.CreateWeaponEvent += MakeWeaponObject;
    // }
    // else if(transform.parent.TryGetComponent<BaseCrafting>(out BaseCrafting crafting) == true)  // Created from a Crafting Screen
    // {
    //     crafting.CreateWeaponEvent += MakeWeaponObject;
    // }

    VS.

    Getting the interface they all implement and adding to their unique event

    // transform.parent.GetComponent<CreateWeaponInterface>().CreateWeaponEvent += MakeWeaponObject;
*/

public interface CreateWeaponInterface
{
    public delegate void CreateWeapon(int type, int[] parts);
    public event CreateWeapon CreateWeaponEvent;
}
