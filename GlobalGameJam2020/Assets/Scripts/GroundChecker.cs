using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public float MaxGroundDistance = 0.01f;
    public float Radius = 0.5f;
    public int NumSamples = 8;
    public bool IsGrounded { get; private set; }
    
   void Update()
    {
        Vector3 origin = transform.position;
        IsGrounded = Physics.Raycast(origin, Vector3.down, MaxGroundDistance);

        float step = 360f / (float)NumSamples;
        for(int i = 0; i < NumSamples; i++)
        {
            if (IsGrounded) return;

            Vector3 offset = Vector3.forward * Radius;
            offset = Quaternion.AngleAxis(step * i, Vector3.up) * offset;
            IsGrounded = IsGrounded || Physics.Raycast(origin + offset, Vector3.down, MaxGroundDistance) ;
        }
    }
}
