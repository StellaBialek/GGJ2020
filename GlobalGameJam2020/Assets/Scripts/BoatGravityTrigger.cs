using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatGravityTrigger : MonoBehaviour
{ 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrel")
        {
            print("1");
            // GetComponent<Rigidbody>().useGravity = true;
            foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            print("2");
            // GetComponent<Rigidbody>().useGravity = true;
            foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>()) {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }
}
