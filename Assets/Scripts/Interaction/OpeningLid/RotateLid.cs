using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pretty much rotate the lid until it collides then disable this
//only have this for now because exporting animations wasn't working out for me
public class RotateLid : MonoBehaviour
{
    private bool active;

    void Start()
    {
        active = true;
        transform.gameObject.layer = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        if(active)
        {
            transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, transform.parent.eulerAngles.z - 0.1f);
        }
        else
        {
            this.enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 7)
        {
            active = false;
        }
    }
}
