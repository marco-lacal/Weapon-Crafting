using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banshee44 : MonoBehaviour
{
    private int[] numbers;

    private GameObject temp;
    public testingscope tempScriptReference;

    void Start()
    {
        numbers = new int[6];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(temp != null)
            {
                Destroy(temp);
            }

            for(int i = 0; i < 6; i++)
            {
                numbers[i] = Random.Range(0, 6) + 1;
            }

            temp = Manufactory(numbers);
            tempScriptReference.FindReticle(temp);
        }
    }

    public GameObject Manufactory(int[] numbers)
    {
        string body = "WeaponParts/Weapon" + numbers[1] + "/Body_" + numbers[1];

        GameObject creation = (GameObject)Instantiate(Resources.Load(body), transform);
        // creation.transform.position += new Vector3(1, 1, 1);

        string barrel = "WeaponParts/Weapon" + numbers[0] + "/Barrel_" + numbers[0];

        GameObject creationBarrel = (GameObject)Instantiate(Resources.Load(barrel), creation.transform);
        creationBarrel.transform.position = creation.transform.Find("BarrelToBody").transform.position;

        string grip = "WeaponParts/Weapon" + numbers[2] + "/Grip_" + numbers[2];

        GameObject creationGrip = (GameObject)Instantiate(Resources.Load(grip), creation.transform);
        creationGrip.transform.position = creation.transform.Find("GripToBody").transform.position;

        string magazine = "WeaponParts/Weapon" + numbers[3] + "/Magazine_" + numbers[3];

        GameObject creationMagazine = (GameObject)Instantiate(Resources.Load(magazine), creation.transform);
        //creationMagazine.transform.position = creation.transform.Find("MagazineToBody").transform.position;
        Transform temp = creation.transform.Find("MagazineToBody").transform;
        creationMagazine.transform.position = temp.position;
        creationMagazine.transform.rotation = temp.rotation;

        string scope = "WeaponParts/Weapon" + numbers[4] + "/Scope_" + numbers[4];

        GameObject creationScope = (GameObject)Instantiate(Resources.Load(scope), creation.transform);
        creationScope.transform.position = creation.transform.Find("ScopeToBody").transform.position;
        creationScope.name = "A_Scope";

        string stock = "WeaponParts/Weapon" + numbers[5] + "/Stock_" + numbers[5];

        GameObject creationStock = (GameObject)Instantiate(Resources.Load(stock), creation.transform);
        creationStock.transform.position = creation.transform.Find("StockToBody").transform.position;

        return creation;
    }
}
