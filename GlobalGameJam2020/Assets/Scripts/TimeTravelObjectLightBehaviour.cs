using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelObjectLightBehaviour : TimeTravelObjectBehaviour
{
    public float Smooth = 2;
    private Light lightsource;

    public void Start()
    {
        lightsource = GetComponent<Light>();
    }

    public void Update()
    {
        lightsource.range = Mathf.Lerp(lightsource.range, Parent.IsLocked ? 3 : 0, Smooth * Time.deltaTime); 
    }
}
