using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatGravityTrigger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrel")
            GetComponent<Rigidbody>().useGravity = true;
    }
}
