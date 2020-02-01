using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Transform Viewer; 

    public float SpeedBuildup = 0.5f;
    public float MaxSpeed = 1;
    public float SpeedFalloff = 2f;
    public float TurnSpeed = 5;

    private AxisSpeed axisX;
    private AxisSpeed axisY;

    private List<AxisSpeed> directions;
    private Rigidbody rb;

    void Start()
    {
        axisX = new AxisSpeed("X", 1, SpeedBuildup, SpeedFalloff);
        axisY = new AxisSpeed("Y", 1, SpeedBuildup, SpeedFalloff);

        directions = new List<AxisSpeed>();
        directions.Add(axisX);
        directions.Add(axisY);

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        foreach (AxisSpeed axis in directions)
            axis.Update();

    }
    void FixedUpdate()
    {
        Vector3 z = transform.position - Viewer.position;
        z.y = 0;
        z = Vector3.Normalize(z);
        Vector3 x = Vector3.Cross(Vector3.up, z);
        Vector3 force = (x * axisX.Value + z * axisY.Value) * MaxSpeed;
        rb.velocity = new Vector3(force.x, rb.velocity.y, force.z);

        if(force.magnitude > 0.001)
        {
            Vector3 forward = -Vector3.Normalize(force);
            if (Vector3.Dot(transform.forward, forward) <= -0.999f)
            {
                forward = Vector3.Cross(Vector3.up, forward);
            }
            transform.forward = Vector3.Lerp(transform.forward, forward, Time.fixedDeltaTime * TurnSpeed);
        }
    }
}
