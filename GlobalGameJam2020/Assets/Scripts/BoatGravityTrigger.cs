using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatGravityTrigger : MonoBehaviour
{
    private void Start()
    {
        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        //GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
        // GetComponent<Rigidbody>().useGravity = true;
            foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>()) {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }
}
