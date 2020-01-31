using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;

    public bool InvertX = false;
    public bool InvertY = false;
    public bool InvertZ = false;
    public bool ActivateOnDrag = false;

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

    private class AxisSpeed
    {
        public string Name;
        public float Current;
        public float Max;
        public float Buildup;
        public float Falloff;
        public bool Inverted;

        public bool Active = true;

        public AxisSpeed(string name, float max, float buildup, float falloff, bool inverted)
        {
            Name = name;
            Max = max;
            Buildup = buildup;
            Falloff = falloff;
            Inverted = inverted;
            Current = 0;
        }

        public void Update()
        {
            if (Active)
            {
                float delta = Max * Buildup * Input.GetAxis(Name) * (Inverted ? -1f : 1f);
                Current = Mathf.Clamp(Current + delta, -Max, Max);
            }
            Active = true;
        }

        public void ApplyFalloff()
        {
            Current = Mathf.Lerp(Current, 0, Time.deltaTime * Falloff);
        }
    }

    public void Start()
    {
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
        if (!Input.GetButton("Camera Drag") && ActivateOnDrag)
        {
            axisX.Active = false;
            axisY.Active = false;
        }

        foreach (AxisSpeed axis in directions)
            axis.Update();

        x += axisX.Current * Time.deltaTime;

        y += axisY.Current * Time.deltaTime;
        y = Mathf.Clamp(y, MinY, MaxY);

        z -= axisZ.Current * Time.deltaTime;
        z = Mathf.Clamp(z, MinZ, MaxZ);

        Vector3 offset = Vector3.forward; //TODO: maybe replace with player forward
        offset = Quaternion.AngleAxis(x, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(y, Vector3.Cross(offset, Vector3.up)) * offset;

        transform.position = Target.position + offset * z;
        transform.LookAt(Target);

        foreach (AxisSpeed axis in directions)
            axis.ApplyFalloff();
    } 

    private float GetInput(string axisName, float maxspeed, float buildup, bool invert)
    {
        return maxspeed * buildup * Input.GetAxis(axisName) * (invert ? -1f : 1f);
    }
}
