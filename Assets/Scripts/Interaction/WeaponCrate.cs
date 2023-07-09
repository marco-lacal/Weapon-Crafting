using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrate : Interactable
{

    [SerializeField] private Transform lid;
    [SerializeField] private GameObject padLock;
    [SerializeField] private GameObject weaponPickup;

    private bool isOpening;
    private GameObject topOfLid;
    private RotateLid script;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //"_Lid" is specified as the top object in the children hierarchy and is thus at index 0
        topOfLid = lid.GetChild(0).gameObject;
    }

    public override void Interact(Transform temp)
    {
        base.Interact(temp);

        //so that this interactable object isnt considered anymore
        transform.gameObject.layer = 7;

        Destroy(padLock);

        //enable the BoxCollider that is normally disabled on the top of the crate
        topOfLid.GetComponent<BoxCollider>().enabled = true;

        //now that the BoxCollider is active, rotate the lid
        script = topOfLid.GetComponent<RotateLid>();
        script.enabled = true;

        StartCoroutine(WaitForRotation());
        //StartCoroutine(OpenChest());
    }

    // IEnumerator OpenChest()
    // {
    //     //Vector3 desiredRotation = new Vector3(lid.transform.eulerAngles.x, lid.transform.eulerAngles.y, 0);

    //     while(true)
    //     {
    //         //lid.transform.eulerAngles = Vector3.Lerp(lid.transform.eulerAngles, desiredRotation, 0.005f);
    //         lid.transform.eulerAngles = new Vector3(lid.transform.eulerAngles.x, lid.transform.eulerAngles.y, lid.transform.eulerAngles.z - 1f);

    //         // Check if the lid has collided with an object on the "whatIsGround" layer
    //         Collider[] hitColliders = Physics.OverlapBox(topOfLid.bounds.center, topOfLid.bounds.extents, lid.transform.rotation, LayerMask.GetMask("whatIsGround"));

    //         foreach(Collider temp in hitColliders)
    //         {
    //             Debug.Log(temp.name);
    //         }

    //         if (hitColliders.Length > 0)
    //         {
    //             Debug.Log("Lid collided with ground");
    //             break; // Exit the loop if the lid has collided with an object on the desired layer
    //         }

    //         yield return null;
    //     }
    //     //create the weaponpickup
    // }

    IEnumerator WaitForRotation()
    {
        //this will remain true as long as the RotateLid script is active
        while(script.enabled)
        {
            yield return null;
        }

        //Rotation has stopped. now create weaponpickup
        //Instantiate a weaponPickup
        GameObject wP = Instantiate(weaponPickup, transform.position + Vector3.up, Quaternion.identity);
        
        //to match the direction of the crate
        wP.transform.localEulerAngles = transform.localEulerAngles;
    }
}
