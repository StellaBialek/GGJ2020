using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool InvertX = false;
    public bool InvertY = false;
    public bool InvertZ = false;

    public float InitialX = 0;
    public float InitialY = 20;
    public float InitialZ = 3;

    public float MinZ = 1;
    public float MaxZ = 5;

    public float MinY = 0;
    public float MaxY = 60;

    public float BuildupX = 0.5f;
    public float BuildupY = 0.5f;
    public float BuildupZ = 0.5f;

    public float MaxSpeedX = 120;
    public float MaxSpeedY = 100;
    public float MaxSpeedZ = 100;

    public float SpeedFalloff = 2f;

    private float x;
    private float y;
    private float z;

    private AxisSpeed axisX;
    private AxisSpeed axisY;
    private AxisSpeed axisZ;
    private List<AxisSpeed> directions;

    private Transform target;

    public void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;

        x = InitialX;
        y = InitialY;
        z = InitialZ;

        axisX = new AxisSpeed("Camera X", MaxSpeedX, BuildupX, SpeedFalloff, InvertX);
        axisY = new AxisSpeed("Camera Y", MaxSpeedY, BuildupY, SpeedFalloff, InvertY);
        axisZ= new AxisSpeed("Camera Z", MaxSpeedZ, BuildupZ, SpeedFalloff, InvertZ);

        directions = new List<AxisSpeed>();
        directions.Add(axisX);
        directions.Add(axisY);
        directions.Add(axisZ);
    }

    public void Update()
    {
        bool joystickMovement = Mathf.Abs(Input.GetAxis("Camera Active")) >= 0.01f;
        bool mouseDown = Input.GetButton("Camera Active");

        if (!(mouseDown || joystickMovement))
        {
            axisX.Active = false;
            axisY.Active = false;;
        }

        foreach(AxisSpeed axis in directions)
        {
            axis.Update();
        }

        x += axisX.Current * Time.deltaTime;

        y += axisY.Current * Time.deltaTime;
        y = Mathf.Clamp(y, MinY, MaxY);

        z -= axisZ.Current * Time.deltaTime;
        z = Mathf.Clamp(z, MinZ, MaxZ);

        Vector3 offset = Vector3.forward;
        offset = Quaternion.AngleAxis(x, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(y, Vector3.Cross(offset, Vector3.up)) * offset;

        transform.position = target.position + offset * z;
        transform.LookAt(target);
    }
}
