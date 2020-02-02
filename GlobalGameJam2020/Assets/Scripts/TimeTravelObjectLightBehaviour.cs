using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelObjectLightBehaviour : TimeTravelObjectBehaviour
{
    private Light lightsource;

    public void Start()
    {
        lightsource = GetComponent<Light>();
    }

    public void Update()
    {
        lightsource.intensity = Mathf.Lerp(lightsource.intensity, Parent.IsLocked ? 1 : 0, Smooth * Time.deltaTime); 
    }
}
