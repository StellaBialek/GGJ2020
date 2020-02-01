using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSpeed
{
    public string Name { get; set; }
    public float Current { get; set; }
    public float Max { get; set; }
    public float Buildup{ get; set; }
    public float Falloff { get; set; }
    public bool Inverted { get; set; }
    public bool Active { get; set; }
    public float Value { get; set; }

    public float Raw { get { return Input.GetAxis(Name); } }

    public AxisSpeed(string name, float max, float buildup, float falloff, bool inverted = false)
    {
        Name = name;
        Max = max;
        Buildup = buildup;
        Falloff = falloff;
        Inverted = inverted;
        Current = 0;
        Value = Current;
        Active = true;
    }

    public void Update()
    {
        if (Active)
        {
            float delta = Max * Buildup * Input.GetAxis(Name) * (Inverted ? -1f : 1f);
            Current = Mathf.Clamp(Current + delta, -Max, Max);
        }
        Value = Current;
        Current = Mathf.Lerp(Current, 0, Time.deltaTime * Falloff);
        Active = true;
    }
}
