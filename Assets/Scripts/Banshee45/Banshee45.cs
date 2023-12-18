using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Banshee45 : MonoBehaviour
{
    //min usually will equal 0 but can be set to something else to restrict the weapon drops
    [SerializeField] private int min;
    private int[] weaponParts;
    public int[] WeaponParts {get {return weaponParts;} }

    public int[] RandomNumbers(int parts, int numWeapons)
    {
        int[] weaponPartNums = new int[parts];

        for(int i = 0; i < weaponPartNums.Length; i++)
        {
            weaponPartNums[i] = Random.Range(min, numWeapons) + 1;
        }

        return weaponPartNums;
    }

    //we take in the transform that will be the parent of all the weapon parts, then we return the relative position the body/weapon should be in when equipped
    public Vector3 Creation(Transform weapon, int[] specificParts, int weaponType)
    {
        switch(weaponType)
        {
            case 0:
                return RifleCreation(weapon, specificParts);
            case 1:
                return SMGCreation(weapon, specificParts);
            case 2:
                return PistolCreation(weapon, specificParts);
            case 3:
                return SwordCreation(weapon, specificParts);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    public Vector3 RifleCreation(Transform weapon, int[] specificParts)
    {
        weaponParts = specificParts;

        if(weaponParts == null)
        {
            weaponParts = RandomNumbers(6, 8);
        }

        //LOAD ALL THE PIECES FIRST

        //First is Barrel
        string barrel = "WeaponParts/Rifle/Weapon" + weaponParts[0] + "/Barrel_" + weaponParts[0];
        GameObject creationBarrel = (GameObject)Instantiate(Resources.Load(barrel), weapon);

        //Second is Body
        string body = "WeaponParts/Rifle/Weapon" + weaponParts[1] + "/Body_" + weaponParts[1];
        GameObject creation = (GameObject)Instantiate(Resources.Load(body), weapon);

        //Send this to the weapon script so we can adjust to the correct position later
        Vector3 inherentPosition = creation.transform.localPosition;

        creation.transform.localPosition = Vector3.zero;

        //Third is Grip
        string grip = "WeaponParts/Rifle/Weapon" + weaponParts[2] + "/Grip_" + weaponParts[2];
        GameObject creationGrip = (GameObject)Instantiate(Resources.Load(grip), weapon);

        //Fourth is Magazine
        string magazine = "WeaponParts/Rifle/Weapon" + weaponParts[3] + "/Magazine_" + weaponParts[3];
        GameObject creationMagazine = (GameObject)Instantiate(Resources.Load(magazine), weapon);

        //Fifth is Scope
        string scope = "WeaponParts/Rifle/Weapon" + weaponParts[4] + "/Scope_" + weaponParts[4];
        GameObject creationScope = (GameObject)Instantiate(Resources.Load(scope), weapon);

        //Finally, sixth is Stock
        string stock = "WeaponParts/Rifle/Weapon" + weaponParts[5] + "/Stock_" + weaponParts[5];
        GameObject creationStock = (GameObject)Instantiate(Resources.Load(stock), weapon);

        //SET LOCATIONS, ROTATIONS, AND PARENTS

        creation.transform.parent = weapon;
        //creation.transform.position = transform.position;

        creationBarrel.transform.parent = weapon;
        creationGrip.transform.parent = weapon;
        creationMagazine.transform.parent = weapon;
        creationScope.transform.parent = weapon;
        creationStock.transform.parent = weapon;

        //Will need to eventually go back into blender and make sure all the imported parts have the same rotation
        //Mostly barrels and stocks

        creationBarrel.transform.position = creation.GetComponent<Body>().BarrelToBody.position;
        //creationBarrel.transform.rotation = creation.GetComponent<Body>().BarrelToBody.rotation;

        creationGrip.transform.position = creation.GetComponent<Body>().GripToBody.position;
        //creationGrip.transform.rotation = creation.GetComponent<Body>().GripToBody.rotation;

        creationMagazine.transform.position = creation.GetComponent<Body>().MagazineToBody.position;
        creationMagazine.transform.rotation = creation.GetComponent<Body>().MagazineToBody.rotation;

        creationScope.transform.position = creation.GetComponent<Body>().ScopeToBody.position;
        //creationScope.transform.rotation = creation.GetComponent<Body>().ScopeToBody.rotation;

        creationStock.transform.position = creation.GetComponent<Body>().StockToBody.position;
        //creationStock.transform.rotation = creation.GetComponent<Body>().StockToBody.rotation;

        return inherentPosition;
    }

    public Vector3 SMGCreation(Transform weapon, int[] specificParts)
    {
        return new Vector3(0, 0, 0);
    }

    public Vector3 PistolCreation(Transform weapon, int[] specificParts)
    {
        return new Vector3(0, 0, 0);
    }

    public Vector3 SwordCreation(Transform weapon, int[] specificParts)
    {
        weaponParts = specificParts;

        if(weaponParts == null)
        {
            weaponParts = RandomNumbers(4, 4);
        }

        // LOAD ALL PIECES FIRST

        string handle = "WeaponParts/Sword/Weapon" + weaponParts[0] + "/Handle_" + weaponParts[0];
        GameObject creationHandle = (GameObject)Instantiate(Resources.Load(handle), weapon);

        string guard = "WeaponParts/Sword/Weapon" + weaponParts[1] + "/Guard_" + weaponParts[1];
        GameObject creationGuard = (GameObject)Instantiate(Resources.Load(guard), weapon);

        string blade = "WeaponParts/Sword/Weapon" + weaponParts[2] + "/Blade_" + weaponParts[2];
        GameObject creationBlade = (GameObject)Instantiate(Resources.Load(blade), weapon);

        string pommel = "WeaponParts/Sword/Weapon" + weaponParts[3] + "/Pommel_" + weaponParts[3];
        GameObject creationPommel = (GameObject)Instantiate(Resources.Load(pommel), weapon);

        Vector3 inherentPosition = creationHandle.transform.localPosition;

        creationHandle.transform.localPosition = Vector3.zero;

        creationHandle.transform.parent = weapon;

        creationPommel.transform.position = creationHandle.GetComponent<Handle>().PtoH.position;
        creationGuard.transform.position = creationHandle.GetComponent<Handle>().GtoH.position;
        creationBlade.transform.position = creationGuard.GetComponent<Guard>().BtoG.position;

        return inherentPosition;
    }
}