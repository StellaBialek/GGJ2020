using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Climbable : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Climber climbing = other.gameObject.GetComponent<Climber>();
        if(climbing)
        {
            climbing.AddClimbable();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Climber climbing = other.gameObject.GetComponent<Climber>();
        if (climbing)
        {
            climbing.RemoveClimbable();
        }
    }
}
