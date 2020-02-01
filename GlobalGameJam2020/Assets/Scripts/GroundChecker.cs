using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public float MaxGroundDistance = 0.01f;
    public float Radius = 0.5f;
    public int NumSamples = 8;
    public bool IsGrounded { get; private set; }
    public Vector3 GroundNormal { get; private set; }
    
   void Update()
    {
        GroundNormal = Vector3.up;
        IsGrounded = false;

        Vector3 origin = transform.position;
        float step = 360f / (float)NumSamples;
        for(int i = 0; i < NumSamples; i++)
        {
            Vector3 offset = Vector3.forward * Radius;
            offset = Quaternion.AngleAxis(step * i, Vector3.up) * offset;

            RaycastHit hit;
            bool grounded = Physics.Raycast(origin + offset, Vector3.down, out hit, MaxGroundDistance);

            if(grounded)
            {
                IsGrounded = true;
                GroundNormal += hit.normal;
            }
        }
        GroundNormal = Vector3.Normalize(GroundNormal);
    }
}
