using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banshee45 : MonoBehaviour
{
    public int[] RandomNumbers()
    {
        int[] weaponPartNums = new int[6];

        for(int i = 0; i < weaponPartNums.Length; i++)
        {
            weaponPartNums[i] = Random.Range(0, 7) + 1;
        }

        return weaponPartNums;
    }

    //we take in the transform that will be the parent of all the weapon parts, then we return the relative position the body/weapon should be in when equipped
    public Vector3 Creation(Transform weapon)
    {
        int[] weaponParts = RandomNumbers();

        //LOAD ALL THE PIECES FIRST

        //First is Barrel
        string barrel = "WeaponParts/Weapon" + weaponParts[0] + "/Barrel_" + weaponParts[0];
        GameObject creationBarrel = (GameObject)Instantiate(Resources.Load(barrel), weapon);

        //Second is Body
        string body = "WeaponParts/Weapon" + weaponParts[1] + "/Body_" + weaponParts[1];
        GameObject creation = (GameObject)Instantiate(Resources.Load(body), weapon);

        //Send this to the weapon script so we can adjust to the correct position later
        Vector3 inherentPosition = creation.transform.localPosition;

        creation.transform.localPosition = Vector3.zero;

        //Third is Grip
        string grip = "WeaponParts/Weapon" + weaponParts[2] + "/Grip_" + weaponParts[2];
        GameObject creationGrip = (GameObject)Instantiate(Resources.Load(grip), weapon);

        //Fourth is Magazine
        string magazine = "WeaponParts/Weapon" + weaponParts[3] + "/Magazine_" + weaponParts[3];
        GameObject creationMagazine = (GameObject)Instantiate(Resources.Load(magazine), weapon);

        //Fifth is Scope
        string scope = "WeaponParts/Weapon" + weaponParts[4] + "/Scope_" + weaponParts[4];
        GameObject creationScope = (GameObject)Instantiate(Resources.Load(scope), weapon);

        //Finally, sixth is Stock
        string stock = "WeaponParts/Weapon" + weaponParts[5] + "/Stock_" + weaponParts[5];
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

    public void LoadAllPieces()
    {
        //Maybe try to figure out how you can make a list of the parts then later iterate over all of them
    }
}

