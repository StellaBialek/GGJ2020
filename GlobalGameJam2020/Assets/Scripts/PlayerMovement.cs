using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Climber))]
public class PlayerMovement : MonoBehaviour
{
    public float SpeedBuildup = 0.5f;
    public float MaxSpeed = 1;
    public float SpeedFalloff = 2f;
    public float TurnSpeed = 5;

    private AxisSpeed sidewaysSpeed;
    private AxisSpeed forwardSpeed;

    private List<AxisSpeed> directions;

    private Transform viewer;
    private Rigidbody rb;
    private Climber c;
    private GroundChecker g;

    void Start()
    {
        viewer = FindObjectOfType<CameraMovement>().transform;

        sidewaysSpeed = new AxisSpeed("X", 1, SpeedBuildup, SpeedFalloff);
        forwardSpeed = new AxisSpeed("Y", 1, SpeedBuildup, SpeedFalloff);

        directions = new List<AxisSpeed>();
        directions.Add(sidewaysSpeed);
        directions.Add(forwardSpeed);

        rb = GetComponent<Rigidbody>();
        c = GetComponent<Climber>();
        g = GetComponentInChildren<GroundChecker>();
    }

    private void Update()
    {
        foreach (AxisSpeed axis in directions)
        {
            axis.Update();
        }
    }
    void FixedUpdate()
    {
        Vector3 forward = transform.position - viewer.position;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        Vector3 sideways = Vector3.Cross(Vector3.up, forward);

        Vector3 forwardForce = forward * forwardSpeed.Value * MaxSpeed;
        Vector3 sidewaysForce = sideways * sidewaysSpeed.Value * MaxSpeed;

        Vector3 viewDirection = transform.forward;

        if (c.IsClimbing)
        {
            float climbingSpeed = forwardSpeed.Value * MaxSpeed * c.ClimbingSpeed;
            Vector3 force = sidewaysForce;
            if(g.IsGrounded && forwardSpeed.Value < 0) //allow walking backwards when on the ground
            {
                force += forwardForce;
            }
            rb.velocity = new Vector3(force.x, climbingSpeed, force.z);
            viewDirection = c.Forward;
        }
        else
        {
            Vector3 force = forwardForce + sidewaysForce;
            rb.velocity = new Vector3(force.x, rb.velocity.y, force.z);

            if (force.magnitude > 0.001)
            {
                 
               viewDirection = -Vector3.Normalize(force);
                if (Vector3.Dot(transform.forward, viewDirection) <= -0.999f)
                {
                    viewDirection = Vector3.Cross(Vector3.up, viewDirection);
                }
            }

            rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
        }
        transform.forward = Vector3.Lerp(transform.forward, viewDirection, Time.fixedDeltaTime * TurnSpeed);
    }
}
